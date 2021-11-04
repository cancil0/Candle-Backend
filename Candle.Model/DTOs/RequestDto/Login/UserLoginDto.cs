using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candle.Model.DTOs.RequestDto.Login
{
    public class UserLoginDto
    {
        public string Email { get; set; }

        public string MobilePhone { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string PrivateTokenKey { get; set; }
    }
}
