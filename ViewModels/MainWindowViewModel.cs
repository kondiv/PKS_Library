using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.Factories;
using PKS_Library.Services.Realisations;

namespace PKS_Library.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private NavigationService _navigationService = null!;

        private readonly PageViewModelFactory _pageViewModelFactory = null!;

        //Конструктор только для preview
        public MainWindowViewModel()
        {
            
        }

        public MainWindowViewModel(NavigationService navigationService, PageViewModelFactory factory)
        {
            _navigationService = navigationService;
            _pageViewModelFactory = factory;

            GoToBooksPage();
        }

        [RelayCommand]
        public void GoToBooksPage()
        {
            var booksPage = _pageViewModelFactory.GetPageViewModel(Data.PageName.Books);

            NavigationService.NavigateTo(booksPage);
        }

        [RelayCommand]
        public void GoToAuthorsPage()
        {
            var authorsPage = _pageViewModelFactory.GetPageViewModel(Data.PageName.Authors);    

            NavigationService.NavigateTo(authorsPage);
        }

        [RelayCommand]
        public void GoToGenresPage()
        {
            var genresPage = _pageViewModelFactory.GetPageViewModel(Data.PageName.Genres);

            NavigationService.NavigateTo(genresPage);
        }
    }
}
