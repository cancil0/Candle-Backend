using System;

namespace Candle.Model.DTOs.RequestDto.User
{
    public class UserRequestDto
    {
        public string Name { get; set; }

        public string SurName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public string SecondaryEmail { get; set; }

        public string MobilePhone { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public string ProfileStatus { get; set; }
    }
}
