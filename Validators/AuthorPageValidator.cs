using FluentValidation;
using PKS_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PKS_Library.Validators
{
    public class AuthorPageValidator : AbstractValidator<AuthorValidationRequest>
    {
        public AuthorPageValidator()
        {
            RuleFor(a => a.FirstName)
                .NotEmpty().WithMessage("Укажите имя автора");

            RuleFor(a => a.FirstName)
                .Must(BeValidName).WithMessage("Имя должно состоять только из букв")
                    .Unless(a => string.IsNullOrEmpty(a.FirstName));

            RuleFor(a => a.LastName)
                .NotEmpty().WithMessage("Укажите фамилию автора");

            RuleFor(a => a.LastName)
                .Must(BeValidName).WithMessage("Фамилия должна состоять только из букв")
                    .Unless(a => string.IsNullOrEmpty(a.LastName));

            RuleFor(a => a.Birthdate)
                .NotEmpty().WithMessage("Укажите дату рождения");

            RuleFor(a => a.Birthdate)
                .Must(BeValidDate).WithMessage("Неверный формат даты dd-MM-yyyy")
                    .Unless(a => string.IsNullOrEmpty(a.Birthdate));

            RuleFor(a => a.Country)
                .NotEmpty().WithMessage("Укажите страну автора");

            RuleFor(a => a.Country)
                .Must(BeValidCountry).WithMessage("Название страны должно состоять только из букв")
                    .Unless(a => string.IsNullOrEmpty(a.Country));
        }

        private static bool BeValidName(string name)
        {
            return name.All(ch => char.IsLetter(ch) || ch == ' ' || ch == '\'') && char.IsLetter(name.First()) && char.IsLetter(name.Last());
        }

        private static bool BeValidDate(string birthdate)
        {
            var currentDate = DateTime.Now;
            var currentDateOnly = DateOnly.FromDateTime(currentDate);

            return DateOnly.TryParse(birthdate, out DateOnly value) && value < currentDateOnly; 
        }

        private static bool BeValidCountry(string country)
        {
            return country.All(ch => char.IsLetter(ch) || ch == ' ') && char.IsLetter(country.First()) && char.IsLetter(country.Last());
        }
    }
}
