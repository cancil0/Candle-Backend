using Candle.Model.DTOs.RequestDto.Login;
using FluentValidation;

namespace Candle.Business.Validation.DtoValidation.RequestDtoValidation.User
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull()
                .When(x => string.IsNullOrEmpty(x.MobilePhone) && string.IsNullOrEmpty(x.UserName))
                .WithMessage("Lütfen email telefon numarası ya da kullanıcı adı giriniz");
            RuleFor(x => x.MobilePhone).Matches("^[0-9]*$").Length(10)
                .When(x => string.IsNullOrEmpty(x.UserName) && string.IsNullOrEmpty(x.Email))
                .WithMessage("Telefon numarası 10 haneli olmak zorundadır");
            RuleFor(x => x.UserName).Length(6, 25).NotNull().NotEmpty()
                .When(x => string.IsNullOrEmpty(x.MobilePhone) && string.IsNullOrEmpty(x.Email))
                .WithMessage("Kullanıcı adı en az altı hane içermelidir");
            RuleFor(x => x.PrivateTokenKey).NotNull().NotEmpty()
                .WithMessage("Private token can not bu null");
        }
    }
}
