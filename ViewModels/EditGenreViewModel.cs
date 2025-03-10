using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.Data;
using PKS_Library.Factories;
using PKS_Library.Models;
using PKS_Library.Services.Interfaces;
using PKS_Library.Services.Realisations;
using PKS_Library.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.ViewModels
{
    public partial class EditGenreViewModel : PageViewModel
    {
        private readonly IGenreService _genreService;

        private readonly NavigationService _navigationService;

        private readonly PageViewModelFactory _factory;

        private Genre _genre = new();
        //поля

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;

        //ошибки
        [ObservableProperty]
        private string _nameError = string.Empty;

        [ObservableProperty]
        private string _descriptionError = string.Empty;

        [ObservableProperty]
        private string _fatalError = string.Empty;

        private bool _areErrorsExist = false;

        public EditGenreViewModel(IGenreService genreService, NavigationService navigationService, PageViewModelFactory factory)
        {
            PageName = Data.PageName.GenreEdit;

            _genreService = genreService;
            _navigationService = navigationService;
            _factory = factory;
        }

        public void SetGenre(Genre genre)
        {
            _genre = genre;

            Name = genre.Name;
            Description = genre.Description;
        }


        [ObservableProperty]
        private string _successMessage = string.Empty;

        [RelayCommand]
        private void OpenAllGenresPage()
        {
            var allGenresPage = _factory.GetPageViewModel(Data.PageName.Genres);

            _navigationService.NavigateTo(allGenresPage);
        }

        [RelayCommand]
        private async Task EditGenre()
        {
            var genreValidator = new GenrePageValidator();

            var validationResult = genreValidator.Validate(new GenreValidationRequest(Name, Description));

            if (!validationResult.IsValid)
            {
                SetErrors(validationResult);
                return;
            }

            if (_areErrorsExist)
            {
                ClearErrors();
            }

            try
            {
                _genre.Name = Name;
                _genre.Description = Description;

                await _genreService.UpdateGenreAsync(_genre);

                SuccessMessage = "Жанр успешно создан";
            }
            catch (Exception ex)
            {
                FatalError = ex.Message;
            }
        }

        private void SetErrors(FluentValidation.Results.ValidationResult validationResult)
        {
            _areErrorsExist = true;

            ClearErrors();

            foreach (var error in validationResult.Errors)
            {
                switch (error.PropertyName)
                {
                    case nameof(Name):
                        NameError = error.ErrorMessage;
                        break;
                    case nameof(Description):
                        DescriptionError = error.ErrorMessage;
                        break;
                }
            }
        }

        private void ClearErrors()
        {
            NameError = string.Empty;
            DescriptionError = string.Empty;
            FatalError = string.Empty;
        }
    }
}
