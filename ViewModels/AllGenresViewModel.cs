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
    public partial class AllGenresViewModel : PageViewModel
    {
        private readonly IGenreService _genreService;

        private readonly NavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<Genre> _genres = [];

        public AllGenresViewModel(IGenreService genreService, NavigationService navigationService)
        {
            PageName = Data.PageName.Genres;

            _genreService = genreService;
            _navigationService = navigationService;

            LoadGenresAsync().ConfigureAwait(false);
        }

        private async Task LoadGenresAsync()
        {
            var genres = await _genreService.GetAllGenresAsync();
            Genres     = new ObservableCollection<Genre>(genres);
        }

        [RelayCommand]
        public void OpenEditGenrePage()
        {

        }
    }
}
