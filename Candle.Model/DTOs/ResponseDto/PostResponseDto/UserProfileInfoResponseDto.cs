using System;

namespace Candle.Model.DTOs.ResponseDto.PostResponseDto
{
    public class UserProfileInfoResponseDto
    {
        public string UserNameSurname { get; set; }

        public string ProfilePhotoPath { get; set; }
        public Guid UserId { get; set; }
    }
}
