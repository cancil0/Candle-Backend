using Candle.Model.Entities;
using FluentValidation;

namespace Candle.Business.EntityValidation.Validation
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(x => x.CommentText).Length(1,250).WithMessage("Yorum alanına 250 karakterden fazla yazamazsınız");
        }
    }
}
