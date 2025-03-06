using FluentValidation;
using PKS_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Validators;

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
            .NotEmpty().WithMessage("Необходимо указать ISBN код книги");

        RuleFor(b => b.Isbn)
            .Must(BeCorrectLengthISBN).WithMessage("Код ISBN должен содержать 13 цифр")
                .Unless(b => string.IsNullOrEmpty(b.Isbn))
            .Must(BeCorrectISBN).WithMessage("Код ISBN должен содержать только цифры")
                .Unless(b => string.IsNullOrEmpty(b.Isbn));

        RuleFor(b => b.PublishYear)
            .NotEmpty().WithMessage("Укажите год издания книги");

        RuleFor(b => b.PublishYear)
            .Must(BeCorrectNumber).WithMessage("Укажите корректное значение")
                .Unless(b => string.IsNullOrEmpty(b.PublishYear));

        RuleFor(b => b.QuantityInStock)
            .NotEmpty().WithMessage("Укажите количество книг в наличии");

        RuleFor(b => b.QuantityInStock)
            .Must(BeCorrectNumber).WithMessage("Укажите корректное значение")
                .Unless(b => string.IsNullOrEmpty(b.QuantityInStock));
    }

    private static bool BeCorrectNumber(string number)
    {
        int currentYear = DateTime.Now.Year;

        return int.TryParse(number, out int value) && value > 0  && value <= currentYear;
    }

    private static bool BeCorrectISBN(string isbn)
    {
        return isbn.All(ch => char.IsDigit(ch) || ch == '-') && char.IsDigit(isbn.First()) && char.IsDigit(isbn.Last());
    }

    private static bool BeCorrectLengthISBN(string isbn)
    {
        const int NeededLength = 13;
        int actualLength = isbn.Count(ch => char.IsDigit(ch));

        return actualLength == NeededLength;
    }
}
