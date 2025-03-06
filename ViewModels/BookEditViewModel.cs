using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.Factories;
using PKS_Library.Models;
using PKS_Library.Services.Interfaces;
using PKS_Library.Services.Realisations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKS_Library.ViewModels
{
    public partial class BookEditViewModel : PageViewModel
    {
        private readonly IBookService _bookService;

        private readonly IAuthorService _authorService;

        private readonly IGenreService _genreService;

        private readonly NavigationService _navigationService;

        private readonly PageViewModelFactory _factory;

        [ObservableProperty]
        private Book _selectedBook = new();

        [ObservableProperty]
        private string? _title;

        [ObservableProperty]
        private int _quantityInStock;

        [ObservableProperty]
        private int _publishYear;

        [ObservableProperty]
        private string? _isbn;

        [ObservableProperty]
        private Author? _author;

        [ObservableProperty]
        private Genre? _genre;

        [ObservableProperty]
        private ObservableCollection<Author> _authors = [];

        [ObservableProperty]
        private ObservableCollection<Genre> _genres = [];

        public BookEditViewModel(IBookService bookService, IAuthorService authorService, IGenreService genreService, NavigationService navigationService, PageViewModelFactory factory)
        {
            PageName = Data.PageName.BookEdit;

            _bookService       = bookService;
            _authorService     = authorService;
            _genreService      = genreService;
            _navigationService = navigationService;
            _factory           = factory;

            LoadDataAsync().ConfigureAwait(false);
        }

        private async Task LoadDataAsync()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            Authors = new ObservableCollection<Author>(authors);

            var genres = await _genreService.GetAllGenresAsync();
            Genres = new ObservableCollection<Genre>(genres);
        }

        public void SetBook(Book book)
        {
            SelectedBook = book;

            Title           = book.Title;
            QuantityInStock = book.QuantityInStock;
            Isbn            = book.Isbn;
            PublishYear     = book.PublishYear;
            Author          = book.Author;
            Genre           = book.Genre;
        }

        [RelayCommand]
        public void GoToBooksPage()
        {
            _navigationService.NavigateTo(_factory.GetPageViewModel(Data.PageName.Books));
        }
    }
}
