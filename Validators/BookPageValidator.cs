using FluentValidation;
using PKS_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Validators
{
    public class BookPageValidator : AbstractValidator<BookValidationRequest>
    {
        public BookPageValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Название книги не должно быть пустым")
                .Length(1, 100).WithMessage("Название книги не должно превышать 100 символов");

            RuleFor(b => b.Author)
                .NotEmpty().WithMessage("Выберите автора из списка");

            RuleFor(b => b.Genre)
                .NotEmpty().WithMessage("Выберите жанр из списка");

            RuleFor(b => b.Isbn)
                .NotEmpty().WithMessage("Необходимо указать ISBN код книги")
                .Length(13).WithMessage("Длина кода ISBN должна составлять 13 символов");

            RuleFor(b => b.PublishYear)
                .NotEmpty().WithMessage("Укажите год издания книги")
                .Must(BeCorrectNumber).WithMessage("Укажите корректное значение");

            RuleFor(b => b.QuantityInStock)
                .NotEmpty().WithMessage("Укажите количество книг в наличии")
                .Must(BeCorrectNumber).WithMessage("Укажите корректное значение");
        }

        private static bool BeCorrectNumber(string number)
        {
            if(!int.TryParse(number, out int value) || !(value >= 0))
            {
                return false;
            }

            return true;
        }
    }
}
