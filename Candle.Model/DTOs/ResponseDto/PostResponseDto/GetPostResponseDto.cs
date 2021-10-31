using Candle.Model.DTOs.ResponseDto.CommentResponseDto;
using Candle.Model.DTOs.ResponseDto.LikeResponseDto;
using Candle.Model.DTOs.ResponseDto.MediaResponseDto;
using Candle.Model.DTOs.ResponseDto.TagResponseDto;
using System;
using System.Collections.Generic;

namespace Candle.Model.DTOs.ResponseDto.PostResponseDto
{
    public class GetPostResponseDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        public ICollection<TagPostResponseDto> Tags { get; set; }

        public ICollection<MediaPostResponseDto> Medias { get; set; }

        public ICollection<CommentPostResponseDto> Comments { get; set; }

        public ICollection<LikePostResponseDto> Likes { get; set; }

    }
}
