using Avalonia.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.CustomExceptions;
using PKS_Library.Data;
using PKS_Library.Factories;
using PKS_Library.Models;
using PKS_Library.Repositories.Interfaces;
using PKS_Library.Services.Interfaces;
using PKS_Library.Services.Realisations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.ViewModels;

public partial class AllBooksViewModel : PageViewModel
{
    private NavigationService _navigationService;

    private readonly PageViewModelFactory _pageFactory;

    private readonly IBookService _bookService;

    private readonly IAuthorService _authorService;

    private readonly IGenreService _genreService;

    [ObservableProperty]
    private string _infoMessage = string.Empty;

    [ObservableProperty]
    private ObservableCollection<Book> _books = [];

    [ObservableProperty]
    private ObservableCollection<Author> _authors = [];

    [ObservableProperty]
    private ObservableCollection<Genre> _genres = [];

    [ObservableProperty]
    private ObservableCollection<BookSortBy> _sortTypes = new([
        BookSortBy.None,
        BookSortBy.Title,
        BookSortBy.TitleDescending,
        BookSortBy.Author,
        BookSortBy.AuthorDescending,
        BookSortBy.Genre,
        BookSortBy.GenreDescending,
        ]);

    [ObservableProperty]
    private Author? _authorFilter = null;

    [ObservableProperty]
    private Genre? _genreFilter = null;

    [ObservableProperty]
    private bool _isFilterVisible = false;

    [ObservableProperty]
    private BookSortBy _bookSortBy;

    public AllBooksViewModel(IBookService bookService, IAuthorService authorService, IGenreService genreService, PageViewModelFactory pageFactory, NavigationService navigationService)
    {
        PageName = Data.PageName.Books;

        _bookService       = bookService;
        _pageFactory       = pageFactory;
        _navigationService = navigationService;
        _authorService     = authorService;
        _genreService      = genreService;

       LoadDataAsync().ConfigureAwait(false);
    }

    private async Task LoadDataAsync()
    {
        var allBooks = await _bookService.GetAllBooksAsync();
        Books = new ObservableCollection<Book>(allBooks);

        var authors = await _authorService.GetAllAuthorsAsync();
        Authors = new ObservableCollection<Author>(authors);

        var genres = await _genreService.GetAllGenresAsync();
        Genres = new ObservableCollection<Genre>(genres);
    }

    [RelayCommand]
    public void OpenEditBookPage(Book book)
    {
        var editPage = _pageFactory.GetPageViewModel(Data.PageName.BookEdit) as EditBookViewModel ?? 
            throw new PageDoesNotExistException("Не удалось открыть страницу редактирования книги");

        editPage.SetBook(book);

        _navigationService.NavigateTo(editPage);
    }

    [RelayCommand]
    public void OpenAddBookPage()
    {
        var addPage = _pageFactory.GetPageViewModel(Data.PageName.BookAdd);

        _navigationService.NavigateTo(addPage);
    }

    [RelayCommand]
    public async Task DeleteBook(Book book)
    {
        int id = book.BookId;

        try
        {
            await _bookService.DeleteBookAsync(id);
            Books.Remove(book);
            await ShowTemporaryMessage("Книга удалена");
        }

        catch(NotFoundException ex)
        {
            await ShowTemporaryMessage(ex.Message);
        }
    }

    private async Task ShowTemporaryMessage(string message)
    {
        InfoMessage = message;

        await Task.Delay(1500);

        InfoMessage = string.Empty; 
    }

    [RelayCommand]
    private void ShowFilters()
    {
        IsFilterVisible = !IsFilterVisible;
    }

    [RelayCommand]
    private async Task ClearFilters()
    {
        AuthorFilter = null;
        GenreFilter  = null;
        BookSortBy   = BookSortBy.None;

        await ApplyFilters();
    }

    [RelayCommand]
    private async Task ApplyFilters()
    {
        var filteredBooks = await _bookService.GetBooksAsync(AuthorFilter, GenreFilter, BookSortBy);

        Books = new ObservableCollection<Book>(filteredBooks);
    }
}
