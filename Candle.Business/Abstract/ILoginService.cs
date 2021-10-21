using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Login;
using Candle.Model.DTOs.RequestDto.User;
using Microsoft.Extensions.Configuration;

namespace Candle.Business.Abstract
{
    public interface ILoginService
    {
        IDataResult<string> Login(UserLoginDto userLoginDto, IConfiguration configuration);

        IResult SignUp(UserRequestDto userRequestDto);

        IDataResult<string> GeneratePinForgotPassword(ForgotPasswordRequestDto forgotPassword);

        IResult EnterPinForgotPassword(EnterPinForgotPassRequestDto enterPinForgot);

        IResult ChangePassword(ChangePasswordDto passwordDto);

    }
}
