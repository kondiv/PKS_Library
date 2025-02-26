using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.Models;
using PKS_Library.Services.Interfaces;
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

        [ObservableProperty]
        private Book _selectedBook = new();

        [ObservableProperty]
        private string? _bookAuthor;

        [ObservableProperty]
        private string? _genre;

        [ObservableProperty]
        private string? _title;

        [ObservableProperty]
        private string? _ISBN;

        [ObservableProperty]
        private int _publishYear;

        [ObservableProperty]
        private int _quantityInStock;

        [ObservableProperty]
        private ObservableCollection<Author> _filteredAuthors = [];

        [ObservableProperty]
        private ObservableCollection<Author> _authors = [];
        
        [ObservableProperty]
        private ObservableCollection<Genre> _filteredGenres = [];

        [ObservableProperty]
        private ObservableCollection<Genre> _genres = [];

        public BookEditViewModel(IBookService bookService, IAuthorService authorService, IGenreService genreService)
        {
            PageName       = Data.PageName.BookEdit;
            _bookService   = bookService;
            _authorService = authorService;
            _genreService  = genreService;

            LoadDataAsync().ConfigureAwait(false);

            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(BookAuthor))
                {
                    FilterAuthorsAsync(BookAuthor ?? string.Empty).ConfigureAwait(false);
                }
            };

            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Genre))
                {
                    FilterGenresAsync(Genre ?? string.Empty).ConfigureAwait(false);
                }
            };
        }

        private async Task LoadDataAsync()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            Authors = new ObservableCollection<Author>(authors);
            FilteredAuthors = new ObservableCollection<Author>(Authors);

            var genres = await _genreService.GetAllGenresAsync();
            Genres = new ObservableCollection<Genre>(genres);
            FilteredGenres = new ObservableCollection<Genre>(Genres);
        }

        public void SetBook(Book book)
        {
            SelectedBook = book;

            BookAuthor      = book.Author.FullName;
            Genre           = book.Genre.ToString();
            Title           = book.Title;
            ISBN            = book.Isbn;
            PublishYear     = book.PublishYear;
            QuantityInStock = book.QuantityInStock;
        }

        private CancellationTokenSource _filterCancellationTokenSource = new();

        [RelayCommand]
        private async Task FilterAuthorsAsync(string author)
        {
            _filterCancellationTokenSource?.Cancel();
            _filterCancellationTokenSource = new CancellationTokenSource();

            try
            {

                await Task.Delay(300, _filterCancellationTokenSource.Token);

                if (string.IsNullOrEmpty(author))
                {
                    FilteredAuthors = new ObservableCollection<Author>(Authors ?? []);
                    return;
                }

                var filtered = await Task.Run(() => Authors?.Where(a => a.FullName.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList());

                FilteredAuthors = new ObservableCollection<Author>(filtered ?? []);
            }
            catch (TaskCanceledException) { }
        }

        [RelayCommand]
        private async Task FilterGenresAsync(string genre)
        {
            _filterCancellationTokenSource?.Cancel();
            _filterCancellationTokenSource = new CancellationTokenSource();

            try
            {

                await Task.Delay(300, _filterCancellationTokenSource.Token);

                if (string.IsNullOrEmpty(genre))
                {
                    FilteredGenres = new ObservableCollection<Genre>(Genres ?? []);
                    return;
                }

                var filtered = await Task.Run(() => Genres?.Where(g => g.Name.Contains(genre, StringComparison.OrdinalIgnoreCase)).ToList());

                FilteredGenres = new ObservableCollection<Genre>(filtered ?? []);
            }
            catch (TaskCanceledException) { }
        }
    }
}
