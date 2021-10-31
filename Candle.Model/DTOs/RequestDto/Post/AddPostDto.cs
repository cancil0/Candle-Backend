using Candle.Model.Entities;
using System;
using System.Collections.Generic;

namespace Candle.Model.DTOs.RequestDto.Post
{
    public class AddPostDto
    {
        public string UserName { get; set; }

        public string Content { get; set; }

        public virtual ICollection<Media> Medias { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

    }
}
