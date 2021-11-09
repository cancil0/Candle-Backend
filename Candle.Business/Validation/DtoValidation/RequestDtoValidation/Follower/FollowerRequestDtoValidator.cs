using Candle.Model.DTOs.RequestDto.Follower;
using FluentValidation;

namespace Candle.Business.Validation.DtoValidation.RequestDtoValidation.Follower
{
    public class FollowerRequestDtoValidator : AbstractValidator<FollowerRequestDto>
    {
        public FollowerRequestDtoValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull()
                .WithMessage("UserId can not be null");
            RuleFor(x => x.FollowerId).NotEmpty().NotNull()
                .WithMessage("FollowerId can not be null");
        }
    }
}
