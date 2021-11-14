using Candle.Application.JwtFeatures;
using Candle.Business.Abstract;
using Candle.Common.Result;
using Candle.DataAccess.Abstract;
using Candle.DataAccess.Service;
using Candle.InfraStructure.Persistence;
using Candle.Model.DTOs.RequestDto.Login;
using Candle.Model.DTOs.RequestDto.User;
using Candle.Model.Entities;
using Candle.Model.Enums;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;

namespace Candle.Business.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUserDal userDal;
        private readonly CandleDbContext dbContext;
        public LoginService()
        {
            dbContext = new CandleDbContext();
            userDal = new UserDalService(dbContext);
        }

        public IDataResult<string> Login(UserLoginDto userLoginDto, IConfiguration configuration)
        {
            JwtHandler jwtHandler = new JwtHandler(configuration);
            var predicate = PredicateBuilder.New<User>()
                .Or(x => x.Email == userLoginDto.Email)
                .Or(x => x.UserName == userLoginDto.UserName)
                .Or(x => x.MobilePhone == userLoginDto.MobilePhone)
                .And(x => x.Password == userLoginDto.Password)
                .And(x => x.UserStatus == (short)UserStatusKey.Active);

            var user = userDal.Get(predicate);

            if (user == null || string.IsNullOrEmpty(userLoginDto.PrivateTokenKey))
            {
                return new ErrorDataResult<string>();
            }

            var signingCredentials = jwtHandler.GetSigningCredentials();
            var claims = jwtHandler.GetClaims(user);
            var tokenOptions = jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var check = jwtHandler.IsTokenValid(userLoginDto.PrivateTokenKey, token);

            if(!check)
                return new ErrorDataResult<string>();

            return new SuccessDataResult<string>(token, "success");
        }

        public IResult SignUp(UserRequestDto userRequestDto)
        {
            if (userRequestDto == null)
            {
                return new ErrorResult();
            }

            User user = new()
            {
                BirthDate = userRequestDto.BirthDate,
                Email = userRequestDto.Email,
                SecondaryEmail = userRequestDto.SecondaryEmail,
                MobilePhone = userRequestDto.MobilePhone,
                Name = userRequestDto.Name,
                SurName = userRequestDto.SurName,
                Password = userRequestDto.Password,
                UserName = userRequestDto.UserName,
                Gender = userRequestDto.Gender,
                UserStatus = (short)UserStatusKey.NeedConfirm,
            };

            userDal.Insert(user);

            return new SuccessResult();
        }

        public IDataResult<string> GeneratePinForgotPassword(ForgotPasswordRequestDto forgotPassword)
        {
            DbSet<PinForgotPassword> entity =  dbContext.Set<PinForgotPassword>();
            if (forgotPassword == null)
            {
                return new ErrorDataResult<string>();
            }

            var predicate = PredicateBuilder.New<User>()
                .Or(x => x.Email == forgotPassword.Email)
                .Or(x => x.UserName == forgotPassword.UserName)
                .Or(x => x.MobilePhone == forgotPassword.MobilePhone);

            var user = userDal.Get(predicate);

            if (user == null)
            {
                return new ErrorDataResult<string>();
            }

            PinForgotPassword lastPin = entity.SingleOrDefault(x => x.UserId == user.Id);

            if (lastPin != null) 
            {
                entity.Remove(lastPin);
                dbContext.SaveChanges();
            }

            Random random = new();
            string randomNumber = random.Next(100000,999999).ToString();

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            File.Create(Path.Combine(docPath, user.UserName + " Pin.txt")).Close();
            using StreamWriter outputFile = new(Path.Combine(docPath, user.UserName + " Pin.txt"), true);
            outputFile.WriteLine("UserName:  " + user.UserName);
            outputFile.WriteLine("Pin:  " + randomNumber);
            outputFile.WriteLine("Date:  " + DateTime.Now.ToString("dd.MM.yyyy"));
            outputFile.WriteLine("Time:  " + DateTime.Now.ToString("HH:mm"));
            outputFile.WriteLine("You have 60 seconds to use this pin");
            outputFile.WriteLine("Please do not share your pin with anybody");
            

            PinForgotPassword pinForgotPassword = new()
            {
                UserId = user.Id,
                Pin = randomNumber,
            };

            entity.Add(pinForgotPassword);
            dbContext.SaveChanges();

            return new SuccessDataResult<string>(user.UserName, "success");
        }

        public IResult EnterPinForgotPassword(EnterPinForgotPassRequestDto enterPinForgot)
        {
            DbSet<PinForgotPassword> entity = dbContext.Set<PinForgotPassword>();

            PinForgotPassword userPinRequest = (from a in entity
                                   where a.User.UserName == enterPinForgot.UserName && 
                                       a.Pin == enterPinForgot.Pin
                                   select a).SingleOrDefault();

            if(userPinRequest == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        public IResult ChangePassword(ChangePasswordDto passwordDto)
        {
            var user = userDal.Get(x => x.UserName == passwordDto.UserName);
            user.Password = passwordDto.Password;
            userDal.Update(user);

            return new SuccessResult();
        }
    }
}
