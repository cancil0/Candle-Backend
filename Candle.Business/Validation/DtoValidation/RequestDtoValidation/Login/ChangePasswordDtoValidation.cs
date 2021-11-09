using Candle.Model.DTOs.RequestDto.Login;
using FluentValidation;

namespace Candle.Business.Validation.DtoValidation.RequestDtoValidation.Login
{
    public class ChangePasswordDtoValidation : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull()
                .WithMessage("UserId can not be null");
            RuleFor(x => x.Password).NotEmpty().NotNull()
                .WithMessage("FollowerId can not be null");
            RuleFor(x => x.Password).Length(8,50)
                .WithMessage("Password length cannot be less than 8 and greater than 50");
        }
    }
}
