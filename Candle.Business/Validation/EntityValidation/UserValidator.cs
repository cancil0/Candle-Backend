using Candle.Model.Entities;
using FluentValidation;

namespace Candle.Business.EntityValidation.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).Matches("^[a-zA-Z ]*$").Length(3,50).NotEmpty().NotNull().WithMessage("İsim boş bırakılamaz");
            RuleFor(x => x.SurName).Matches("^[a-zA-Z]*$").Length(3, 50).NotEmpty().NotNull().WithMessage("Soyisim boş bırakılamaz");
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull().WithMessage("Lütfen geçerli bir email adresi giriniz");
            RuleFor(x => x.SecondaryEmail).EmailAddress().WithMessage("Lütfen geçerli bir ikincil email adresi giriniz");
            RuleFor(x => x.MobilePhone).Matches("^[0-9]*$").Length(10).WithMessage("Telefon numarası 10 haneli olmak zorundadır");
            RuleFor(x => x.UserName).Length(6, 25).NotNull().NotEmpty().WithMessage("Kullanıcı adı en az altı hane içermelidir");
            RuleFor(x => x.Gender).Length(1).NotNull().NotEmpty();
        }
    }
}
