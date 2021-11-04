using Candle.Application.System;
using Candle.Business.Abstract;
using Candle.Business.Service;
using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Login;
using Candle.Model.DTOs.RequestDto.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Candle.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration configuration;
        public LoginController(IConfiguration configuration)
        {
            _loginService = new LoginService();
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<IDataResult<string>> Login([FromBody] UserLoginDto userLoginDto)
        {
            var login = _loginService.Login(userLoginDto, configuration);
            return Ok(login);
        }

        [HttpPost]
        [Route("SignUp")]
        public ActionResult<IResult> SignUp([FromBody] UserRequestDto userRequestDto)
        {
            var signUp = _loginService.SignUp(userRequestDto);
            return Ok(signUp);
        }

        [HttpPost]
        [Route("GeneratePinForgotPassword")]
        public ActionResult<IDataResult<string>> GeneratePinForgotPassword([FromBody] ForgotPasswordRequestDto forgotPassword)
        {
            var pin = _loginService.GeneratePinForgotPassword(forgotPassword);
            return Ok(pin);
        }

        [HttpPost]
        [Route("EnterPinForgotPassword")]
        public ActionResult<IResult> EnterPinForgotPassword([FromBody] EnterPinForgotPassRequestDto enterPinForgot)
        {
            var pin = _loginService.EnterPinForgotPassword(enterPinForgot);
            return Ok(pin);
        }

        [HttpPut]
        [Route("ChangePassword")]
        public ActionResult<IResult> ChangePassword([FromBody] ChangePasswordDto passwordDto)
        {
            var pass = _loginService.ChangePassword(passwordDto);
            return Ok(pass);
        }
    }
}
