using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candle.Model.DTOs.ResponseDto.ProfileResponseDto
{
    public class GetProfileCountDto
    {
        public int Post { get; set; }

        public int Follower { get; set; }

        public int Following { get; set; }
    }
}
