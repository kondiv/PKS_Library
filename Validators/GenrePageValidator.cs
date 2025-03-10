using FluentValidation;
using PKS_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Validators
{
    public class GenrePageValidator : AbstractValidator<GenreValidationRequest>
    {
        public GenrePageValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("Введите название жанра")
                .MaximumLength(50).WithMessage("Название жанра должно быть меньше 50 символов");

            RuleFor(g => g.Description)
                .NotEmpty().WithMessage("Введите описание жанра")
                .MaximumLength(1000).WithMessage("Описание жанра должно быть меньше 1000 символов");
        }
    }
}
