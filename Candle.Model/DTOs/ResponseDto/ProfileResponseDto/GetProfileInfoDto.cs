using System;

namespace Candle.Model.DTOs.ResponseDto.ProfileResponseDto
{
    public class GetProfileInfoDto
    {
        public int Post { get; set; }

        public int Follower { get; set; }

        public int Following { get; set; }

        public string ProfilePhotoPath { get; set; }

        public string UserNameSurname { get; set; }
        public Guid UserId { get; set; }
    }
}
