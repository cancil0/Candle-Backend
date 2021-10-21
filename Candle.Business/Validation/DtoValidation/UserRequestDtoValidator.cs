using Candle.Model.DTOs.RequestDto.User;
using FluentValidation;

namespace Candle.Business.Validation.DtoValidation
{
    public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
    {
        public UserRequestDtoValidator()
        {
            
        }
    }
}
