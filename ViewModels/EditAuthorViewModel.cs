using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.Factories;
using PKS_Library.Models;
using PKS_Library.Services.Interfaces;
using PKS_Library.Services.Realisations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.ViewModels
{
    public partial class EditAuthorViewModel : PageViewModel
    {
        private readonly IAuthorService _authorService;

        private readonly NavigationService _navigationService;

        private readonly PageViewModelFactory _factory;

        private Author _selectedAuthor = new();

        #region Поля формы
        [ObservableProperty]
        private string _firstName = string.Empty;

        [ObservableProperty]
        private string _lastName = string.Empty;

        [ObservableProperty]
        private string _birthdate = string.Empty;

        [ObservableProperty]
        private string _country = string.Empty;
        #endregion

        #region Ошибки
        [ObservableProperty]
        private string _firstNameError = string.Empty;

        [ObservableProperty]
        private string _lastNameError = string.Empty;

        [ObservableProperty]
        private string _birthdateError = string.Empty;

        [ObservableProperty]
        private string _countryError = string.Empty;

        [ObservableProperty]
        private string _fatalError = string.Empty;
        #endregion

        public EditAuthorViewModel(IAuthorService authorService, NavigationService navigationService, PageViewModelFactory factory)
        {
            PageName = Data.PageName.AuthorEdit;

            _authorService = authorService;
            _navigationService = navigationService;
            _factory = factory;
        }

        public void SetAuthor(Author author)
        {
            _selectedAuthor = author;

            FirstName = author.FirstName;
            LastName  = author.LastName;
            Birthdate = author.Birthdate.ToString();
            Country   = author.Country;
        }

        [RelayCommand]
        public void GoToAuthorsPage()
        {
            var authorsPage = _factory.GetPageViewModel(Data.PageName.Authors);

            _navigationService.NavigateTo(authorsPage);
        }
    }
}
