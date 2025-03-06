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
    public partial class AuthorEditViewModel : PageViewModel
    {
        private readonly IAuthorService _authorService;

        private readonly NavigationService _navigationService;

        private readonly PageViewModelFactory _factory;

        private Author? _author;

        [ObservableProperty]
        private string? _firstName;

        [ObservableProperty]
        private string? _lastName;

        [ObservableProperty]
        private DateOnly? _birthdate;

        [ObservableProperty]
        private string? _country;

        public AuthorEditViewModel(IAuthorService authorService, NavigationService navigationService, PageViewModelFactory factory)
        {
            PageName = Data.PageName.AuthorEdit;

            _authorService = authorService;
            _navigationService = navigationService;
            _factory = factory;
        }

        public void SetAuthor(Author author)
        {
            _author = author;

            FirstName = author.FirstName;
            LastName  = author.LastName;
            Birthdate = author.Birthdate;
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
