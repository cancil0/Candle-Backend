using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Login;
using Candle.Model.DTOs.RequestDto.User;
using Candle.Model.DTOs.ResponseDto.PostResponseDto;
using Candle.Model.Entities;
using System;
using System.Linq;

namespace Candle.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> GetById(Guid Id);

        IDataResult<IQueryable<User>> GetAll();

        IResult UpdateUser(UserRequestDto user);

        IResult DeleteUser(UserLoginDto user);

        IResult ActivateUser(string userName);

        UserProfileInfoResponseDto GetUserProfileInfo(string userName);

        void UpdateProfilePhotoPath(string userName, string path);
    }
}
