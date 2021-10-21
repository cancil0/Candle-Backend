using System;

namespace Candle.Model.DTOs.RequestDto.Login
{
    public class ChangePasswordDto
    {
        public string Password { get; set; }

        public string UserName { get; set; }
    }
}
