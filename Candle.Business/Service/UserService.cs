using Candle.Application.System;
using Candle.Business.Abstract;
using Candle.Common.Result;
using Candle.DataAccess.Abstract;
using Candle.DataAccess.Service;
using Candle.InfraStructure.Persistence;
using Candle.Model.DTOs.RequestDto.Login;
using Candle.Model.DTOs.RequestDto.User;
using Candle.Model.DTOs.ResponseDto.PostResponseDto;
using Candle.Model.Entities;
using Candle.Model.Enums;
using Candle.Model.Enums.EnumExtensions;
using LinqKit;
using System;
using System.Linq;

namespace Candle.Business.Service
{
    public class UserService : IUserService
    {
        private readonly IUserDal userDal;
        private readonly IFollowerDal followerDal;
        private readonly CandleDbContext dbContext;
        public UserService()
        {
            dbContext = new CandleDbContext();
            userDal = new UserDalService(dbContext);
            followerDal = new FollowerDalService(dbContext);
        }

        public IDataResult<IQueryable<User>> GetAll()
        {
            var result = userDal.GetAll().Where(x => x.UserStatus == UserStatusKey.Active.GetValue().ToShort());
            return new SuccessDataResult<IQueryable<User>>(result);
        }

        public IDataResult<User> GetById(Guid Id)
        {
            var user = userDal.GetbyId(Id);

            if (user == null || user.IsActive != IsActiveKey.Active.GetValue().ToShort())
            {
                return new ErrorDataResult<User>(user);
            }

            return new SuccessDataResult<User>(user);
        }

        public IResult UpdateUser(UserRequestDto userRequest)
        {
            if (userRequest == null)
            {
                return new ErrorResult();
            }

            var predicate = PredicateBuilder.New<User>()
                .Or(x => x.Email == userRequest.Email)
                .Or(x => x.UserName == userRequest.UserName)
                .Or(x => x.MobilePhone == userRequest.MobilePhone);

            var user = userDal.Get(predicate);

            if (user == null)
            {
                return new ErrorResult();
            }

            string encryptedPass = Encryption.EncryptString(Encryption.encryptionKey, userRequest.Password);

            user.Name = userRequest.Name;
            user.SurName = userRequest.SurName;
            user.BirthDate = userRequest.BirthDate;
            user.Email = userRequest.Email;
            user.MobilePhone = userRequest.MobilePhone;
            user.Password = encryptedPass;
            user.Gender = userRequest.Gender;
            user.SecondaryEmail = userRequest.SecondaryEmail;
            user.UserName = userRequest.UserName;
            user.ProfileStatus = userRequest.ProfileStatus;

            userDal.Update(user);
            return new SuccessResult();
        }

        public IResult DeleteUser(UserLoginDto userRequest)
        {
            if (userRequest == null)
            {
                return new ErrorResult();
            }

            var predicate = PredicateBuilder.New<User>()
                .Or(x => x.Email == userRequest.Email)
                .Or(x => x.UserName == userRequest.UserName)
                .Or(x => x.MobilePhone == userRequest.MobilePhone);

            var user = userDal.Get(predicate);

            if (user == null)
            {
                return new ErrorResult();
            }

            userDal.Delete(user);
            return new SuccessResult();
        }

        public IResult ActivateUser(string userName)
        {
            var user = userDal.Get(x => x.UserName == userName);

            if (user == null || user.IsActive != IsActiveKey.Active.GetValue().ToShort())
            {
                return new ErrorResult("User not found");
            }

            if (user.UserStatus == UserStatusKey.Active.GetValue().ToShort())
            {
                return new ErrorResult("User status already activated");
            }

            if (user.UserStatus == UserStatusKey.NotActive.GetValue().ToShort())
            {
                return new ErrorResult("This user can not activated");
            }

            user.UserStatus = UserStatusKey.Active.GetValue().ToShort();
            userDal.Update(user);

            return new SuccessResult();
        }

        public UserProfileInfoResponseDto GetUserProfileInfo(string userName) {

            var user = userDal.Get(x => x.UserName == userName);

            UserProfileInfoResponseDto userProfileInfo = new()
            {
                UserId = user.Id,
                ProfilePhotoPath = user.ProfilePhotoPath,
                UserNameSurname = string.Format("{0} {1}", user.Name, user.SurName)
            };

            return userProfileInfo;
        }

        public void UpdateProfilePhotoPath(string userName, string path)
        {
            var user = userDal.Get(x => x.UserName == userName);

            if(user.ProfilePhotoPath != path)
            {
                user.ProfilePhotoPath = path;
                userDal.Update(user);
            }
        }

        public Result CheckUserProfileStatus(CheckUserProfileStatusRequestDto statusRequestDto)
        {
            if(statusRequestDto.UserName != statusRequestDto.ProfileOwnerUserName)
            {
                var user = userDal.Get(x => x.UserName == statusRequestDto.UserName);

                if (user.ProfileStatus != ProfileStatusKey.Everyone.GetValue())
                {
                    if (user.ProfileStatus == ProfileStatusKey.Confidential.GetValue())
                    {
                        return new ErrorResult(ProfileStatusKey.Confidential.GetDescription());
                    }

                    if (user.ProfileStatus == ProfileStatusKey.Follower.GetValue())
                    {
                        IQueryable<User> followers = followerDal.GetFollowers(statusRequestDto.UserName);

                        if (!followers.Any(x => x.UserName == statusRequestDto.ProfileOwnerUserName))
                        {
                            return new ErrorResult(ProfileStatusKey.Follower.GetDescription());
                        }

                    }
                }
            }

            return new SuccessResult();
        }
    }
}
