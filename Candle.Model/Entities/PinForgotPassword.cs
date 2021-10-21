using System;

namespace Candle.Model.Entities
{
    public class PinForgotPassword
    {
        public string Pin { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
