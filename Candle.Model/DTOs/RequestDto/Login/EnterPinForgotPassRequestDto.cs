using System;

namespace Candle.Model.DTOs.RequestDto.Login
{
    public class EnterPinForgotPassRequestDto
    {
        public string Pin { get; set; }

        public string UserName { get; set; }
    }
}
