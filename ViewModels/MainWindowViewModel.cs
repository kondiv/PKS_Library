using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.Factories;
using PKS_Library.Services.Realisations;

namespace PKS_Library.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly PageViewModelFactory _factory;

        [ObservableProperty]
        private NavigationService _navigationService;

        //Конструктор только для preview
        public MainWindowViewModel()
        {
            
        }

        public MainWindowViewModel(NavigationService navigationService, PageViewModelFactory pageFactory)
        {
            _navigationService = navigationService;
            _factory = pageFactory;

            GoToBooksPage();
        }

        [RelayCommand]
        public void GoToBooksPage()
        {
            var booksPage = _factory.GetPageViewModel(Data.PageName.Books);

            NavigationService.NavigateTo(booksPage);
        }

        [RelayCommand]
        public void GoToAuthorsPage()
        {
            var authorsPage = _factory.GetPageViewModel(Data.PageName.Authors);

            NavigationService.NavigateTo(authorsPage);
        }

        [RelayCommand]
        public void GoToGenresPage()
        {
            var genresPage = _factory.GetPageViewModel(Data.PageName.Genres);

            NavigationService.NavigateTo(genresPage);
        }
    }
}
