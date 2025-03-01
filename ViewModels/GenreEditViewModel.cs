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
    public partial class GenreEditViewModel : PageViewModel
    {
        private readonly IGenreService _genreService;

        private readonly NavigationService _navigationService;

        private readonly PageViewModelFactory _pageViewModelFactory;

        private Genre? _genre;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _description;

        public GenreEditViewModel(IGenreService genreService, NavigationService navigationService, PageViewModelFactory factory)
        {
            PageName = Data.PageName.GenreEdit;

            _genreService = genreService;
            _navigationService = navigationService;
            _pageViewModelFactory = factory;
        }

        public void SetGenre(Genre genre)
        {
            _genre = genre;

            Name = genre.Name;
            Description = genre.Description;
        }

        [RelayCommand]
        public void GoToGenresPage()
        {
            var genresPage = _pageViewModelFactory.GetPageViewModel(Data.PageName.Genres);

            _navigationService.NavigateTo(genresPage);
        }
    }
}
