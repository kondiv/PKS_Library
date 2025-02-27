using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.Models;
using PKS_Library.Services.Interfaces;
using PKS_Library.Services.Realisations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.ViewModels
{
    public partial class AllAuthorsViewModel : PageViewModel
    {
        private readonly IAuthorService _authorService;

        private readonly NavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<Author> _authors = [];

        public AllAuthorsViewModel(IAuthorService authorService, NavigationService navigationService)
        {
            PageName = Data.PageName.Authors;

            _authorService = authorService;
            _navigationService = navigationService;

            LoadAuthorsAsync().ConfigureAwait(false);
        }

        private async Task LoadAuthorsAsync()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            Authors = new ObservableCollection<Author>(authors);
        }

        [RelayCommand]
        public void OpenEditAuthorPage(Author author)
        {

        }
    }
}
