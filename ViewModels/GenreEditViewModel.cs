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

        public GenreEditViewModel(IGenreService genreService, NavigationService navigationService)
        {
            PageName = Data.PageName.GenreEdit;
        }
    }
}
