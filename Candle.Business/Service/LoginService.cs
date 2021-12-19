using Candle.Application.JwtFeatures;
using Candle.Application.System;
using Candle.Business.Abstract;
using Candle.Common.Result;
using Candle.DataAccess.Abstract;
using Candle.DataAccess.Service;
using Candle.InfraStructure.Persistence;
using Candle.Model.DTOs.RequestDto.Login;
using Candle.Model.DTOs.RequestDto.User;
using Candle.Model.Entities;
using Candle.Model.Enums;
using Candle.Model.Enums.EnumExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Candle.Business.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUserDal userDal;
        private readonly IPinDal pinDal;
        private readonly CandleDbContext dbContext;
        public LoginService()
        {
            dbContext = new CandleDbContext();
            userDal = new UserDalService(dbContext);
            pinDal = new PinDalService(dbContext);
        }

        public IDataResult<string> Login(UserLoginDto userLoginDto, IConfiguration configuration)
        {
            string encryptedPass =  Encryption.EncryptString(Encryption.encryptionKey, userLoginDto.Password);

            var predicate = PredicateBuilder.New<User>()
                .Or(x => x.Email == userLoginDto.Email)
                .Or(x => x.UserName == userLoginDto.UserName)
                .Or(x => x.MobilePhone == userLoginDto.MobilePhone)
                .And(x => x.Password == encryptedPass);

            var user = userDal.Get(predicate);

            if (user == null || string.IsNullOrEmpty(userLoginDto.PrivateTokenKey))
                return new ErrorDataResult<string>();

            if(user.UserStatus == UserStatusKey.NotActive.GetValue().ToShort())
                return new ErrorDataResult<string>(null, "userisbanned");

            JwtHandler jwtHandler = new(configuration);
            var signingCredentials = jwtHandler.GetSigningCredentials();
            var claims = jwtHandler.GetClaims(user);
            var tokenOptions = jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var isTokenValid = jwtHandler.IsTokenValid(userLoginDto.PrivateTokenKey, token);

            if (!isTokenValid)
                return new ErrorDataResult<string>();

            if (user.UserStatus == UserStatusKey.NeedConfirm.GetValue().ToShort())
                return new ErrorDataResult<string>(user.UserName, "needconfirm");

            return new SuccessDataResult<string>(token, "success");
        }

        public IResult SignUp(UserRequestDto userRequestDto)
        {
            if (userRequestDto == null)
            {
                return new ErrorResult();
            }

            string encryptedPass = Encryption.EncryptString(Encryption.encryptionKey, userRequestDto.Password);

            User user = new()
            {
                BirthDate = userRequestDto.BirthDate,
                Email = userRequestDto.Email,
                SecondaryEmail = userRequestDto.SecondaryEmail,
                MobilePhone = userRequestDto.MobilePhone,
                Name = userRequestDto.Name,
                SurName = userRequestDto.SurName,
                Password = encryptedPass,
                UserName = userRequestDto.UserName,
                Gender = userRequestDto.Gender,
                UserStatus = UserStatusKey.NeedConfirm.GetValue().ToShort(),
                ProfileStatus = userRequestDto.ProfileStatus,
                ProfilePhotoPath = userRequestDto.Gender.Equals("M") ? "../../../../assets/defaultProfilePhoto/img_avatar_man.png" 
                                                                     : "../../../../assets/defaultProfilePhoto/img_avatar_woman.png"
            };

            userDal.Insert(user);

            return new SuccessResult();
        }

        public IDataResult<string> GeneratePinForgotPassword(ForgotPasswordRequestDto forgotPassword)
        {
            if (forgotPassword == null)
                return new ErrorDataResult<string>();
            
            var predicate = PredicateBuilder.New<User>()
                .Or(x => x.Email == forgotPassword.Email)
                .Or(x => x.UserName == forgotPassword.UserName)
                .Or(x => x.MobilePhone == forgotPassword.MobilePhone);

            var user = userDal.Get(predicate);

            if (user == null)
                return new ErrorDataResult<string>();

            Random random = new();
            string randomNumber = random.Next(100000, 999999).ToString();

            pinDal.RemoveLastPin(user.Id);
            pinDal.GeneratePinMessageFile(user, randomNumber, forgotPassword.FileResources);
            pinDal.AddNewPin(new() { UserId = user.Id, Pin = randomNumber });

            return new SuccessDataResult<string>(user.UserName, "success");
        }

        public IResult EnterPinForgotPassword(EnterPinForgotPassRequestDto enterPinForgot)
        {
            if(pinDal.IsPinCorrect(enterPinForgot))
                return new SuccessResult();
            
            return new ErrorResult();
        }

        public IResult ChangePassword(ChangePasswordDto passwordDto)
        {
            var user = userDal.Get(x => x.UserName == passwordDto.UserName);

            if(user == null)
                return new ErrorResult();

            string encryptedPass = Encryption.EncryptString(Encryption.encryptionKey, passwordDto.Password);

            user.Password = encryptedPass;
            userDal.Update(user);

            return new SuccessResult();
        }
    }
}
