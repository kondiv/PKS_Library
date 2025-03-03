using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.Builders.Realisations;
using PKS_Library.CustomExceptions;
using PKS_Library.Factories;
using PKS_Library.Models;
using PKS_Library.Services.Interfaces;
using PKS_Library.Services.Realisations;
using PKS_Library.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.ViewModels
{
    public partial class AddBookViewModel : PageViewModel
    {
        private readonly IBookService _bookService;

        private readonly IAuthorService _authorService;

        private readonly IGenreService _genreService;

        private readonly NavigationService _navigationService;

        private readonly PageViewModelFactory _pageViewModelFactory;

        public AddBookViewModel(IBookService bookService, IAuthorService authorService, IGenreService genreService, NavigationService navigationService, PageViewModelFactory factory)
        {
            PageName = Data.PageName.BookAdd;

            _bookService          = bookService;
            _authorService        = authorService;
            _genreService         = genreService;
            _navigationService    = navigationService;
            _pageViewModelFactory = factory;

            LoadDataAync().ConfigureAwait(false);
        }

        [ObservableProperty]
        private List<Author> _authors = [];

        [ObservableProperty]
        private List<Genre> _genres = [];

        private async Task LoadDataAync()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            Authors = new List<Author>(authors);

            var genres = await _genreService.GetAllGenresAsync();
            Genres = new List<Genre>(genres);
        }

        [RelayCommand]
        public void GoToBooksPage()
        {
            var booksPage = _pageViewModelFactory.GetPageViewModel(Data.PageName.Books);

            _navigationService.NavigateTo(booksPage);
        }

        //Сообщение о добавлении книги
        [ObservableProperty]
        private string _successMessage = String.Empty;

        //Поля
        [ObservableProperty]
        private string _title = String.Empty;

        [ObservableProperty]
        private Author _author = null!;

        [ObservableProperty]
        private Genre _genre = null!;
        
        [ObservableProperty]
        private string _isbn = String.Empty;

        [ObservableProperty]
        private string _publishYear = String.Empty;

        [ObservableProperty]
        private string _quantityInStock = String.Empty;

        //Ошибки
        [ObservableProperty]
        private string _titleError = String.Empty;

        [ObservableProperty]
        private string _authorError = String.Empty;

        [ObservableProperty]
        private string _genreError = String.Empty;

        [ObservableProperty]
        private string _isbnError = String.Empty;

        [ObservableProperty]
        private string _publishYearError = String.Empty;

        [ObservableProperty]
        private string _quantityInStockError = String.Empty;

        [ObservableProperty]
        private string _fatalError = String.Empty;

        [RelayCommand]
        public async Task CreateBook()
        {
            var validator = new BookPageValidator();
            var validationResult = validator.Validate(new Data.BookValidationRequest(Title, Author, Genre, Isbn, PublishYear, QuantityInStock));

            if (!validationResult.IsValid)
            {
                SetErrors(validationResult);
                return;
            }

            var book = BuildBook();

            try
            {
                await _bookService.AddBookAsync(book);
                SuccessMessage = "Книга добавлена";
            }

            catch(AlreadyExistsException ex)
            {
                FatalError = ex.Message;
            }
        }

        public void SetErrors(FluentValidation.Results.ValidationResult validationResult)
        {
            ClearErrors();

            foreach (var error in validationResult.Errors)
            {
                switch (error.PropertyName)
                {
                    case nameof(Title):
                        TitleError = error.ErrorMessage;
                        break;

                    case nameof(Author):
                        AuthorError = error.ErrorMessage;
                        break;

                    case nameof(Genre):
                        GenreError = error.ErrorMessage;
                        break;

                    case nameof(Isbn):
                        IsbnError = error.ErrorMessage;
                        break;

                    case nameof(PublishYear):
                        PublishYearError = error.ErrorMessage;
                        break;

                    case nameof(QuantityInStock):
                        QuantityInStockError = error.ErrorMessage;
                        break;

                    default:
                        break;
                }
            }
        }

        private void ClearErrors()
        {
            TitleError           = string.Empty;
            AuthorError          = string.Empty;
            GenreError           = string.Empty;
            IsbnError            = string.Empty;
            PublishYearError     = string.Empty;
            QuantityInStockError = string.Empty;
        }

        private Book BuildBook()
        {
            var bookBuilder = new BookBuilder();

            bookBuilder.SetTitle(Title)
                .SetAuthor(Author)
                .SetGenre(Genre)
                .SetIsbn(Isbn)
                .SetPublishYear(int.Parse(PublishYear))
                .SetQuantityInStock(int.Parse(QuantityInStock));

            return bookBuilder.Build();
        }
    }
}
