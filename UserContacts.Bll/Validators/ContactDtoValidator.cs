using FluentValidation;
using UserContacts.Bll.Dtos;

namespace UserContacts.Bll.Validators
{
    public class ContactDtoValidator : AbstractValidator<ContactDto>
    {
        public ContactDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id musbat bo‘lishi kerak");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ism majburiy")
                .MaximumLength(100).WithMessage("Ism 100 belgidan oshmasligi kerak");

            RuleFor(x => x.LastName)
                .MaximumLength(100).WithMessage("Familiya 100 belgidan oshmasligi kerak");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon raqam majburiy")
                .Matches(@"^\+?\d{9,15}$").WithMessage("Telefon raqam noto‘g‘ri formatda");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email noto‘g‘ri formatda")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("Manzil 200 belgidan oshmasligi kerak");
        }
    }
}
