using Candle.Model.DTOs.RequestDto.User;
using FluentValidation;

namespace Candle.Business.Validation.DtoValidation.RequestDtoValidation.User
{
    public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
    {
        public UserRequestDtoValidator()
        {
            
        }
    }
}
