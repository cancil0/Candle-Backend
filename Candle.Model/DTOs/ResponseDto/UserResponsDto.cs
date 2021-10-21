using Candle.Model.Entities;
using System;
using System.Collections.Generic;

namespace Candle.Model.DTOs.ResponseDto
{
    public class UserResponsDto
    {
        public string Name { get; set; }

        public string SurName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public string SecondaryEmail { get; set; }

        public string MobilePhone { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
