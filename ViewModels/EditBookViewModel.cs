using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.CustomExceptions;
using PKS_Library.Data;
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
using System.Threading;
using System.Threading.Tasks;

namespace PKS_Library.ViewModels;

public partial class EditBookViewModel : PageViewModel
{
    private readonly IBookService _bookService;

    private readonly IAuthorService _authorService;

    private readonly IGenreService _genreService;

    private readonly NavigationService _navigationService;

    private readonly PageViewModelFactory _factory;

    //ошибки
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

    private bool _areErrorsExist = false;

    //поля
    [ObservableProperty]
    private Book _selectedBook = null!;

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _quantityInStock = string.Empty;

    [ObservableProperty]
    private string _publishYear = string.Empty;

    [ObservableProperty]
    private string _isbn = string.Empty;

    [ObservableProperty]
    private Author _author = null!;

    [ObservableProperty]
    private Genre _genre = null!;

    [ObservableProperty]
    private ObservableCollection<Author> _authors = [];

    [ObservableProperty]
    private ObservableCollection<Genre> _genres = [];

    public EditBookViewModel(IBookService bookService, IAuthorService authorService, IGenreService genreService, NavigationService navigationService, PageViewModelFactory factory)
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
        QuantityInStock = book.QuantityInStock.ToString();
        Isbn            = book.Isbn;
        PublishYear     = book.PublishYear.ToString();
        Author          = book.Author;
        Genre           = book.Genre;
    }

    [RelayCommand]
    public void GoToBooksPage()
    {
        _navigationService.NavigateTo(_factory.GetPageViewModel(Data.PageName.Books));
    }

    [ObservableProperty]
    private string _successMessage = string.Empty;

    [RelayCommand]
    private async Task UpdateBook()
    {
        var validator = new BookPageValidator();

        var validationResult = validator.Validate(new BookValidationRequest(Title, Author, Genre, Isbn, PublishYear, QuantityInStock));

        if(!validationResult.IsValid)
        {
            SetErrors(validationResult);
            return;
        }

        if(_areErrorsExist)
        {
            ClearErrors();
        }

        try
        {
            SelectedBook.Title           = Title;
            SelectedBook.Author          = Author;
            SelectedBook.Genre           = Genre;
            SelectedBook.Isbn            = Isbn;
            SelectedBook.PublishYear     = int.Parse(PublishYear);
            SelectedBook.QuantityInStock = int.Parse(QuantityInStock);

            await _bookService.UpdateBookAsync(SelectedBook);

            SuccessMessage = "Данные книги успешно обновлены";
        }
        catch(Exception ex)
        {
            FatalError = ex.Message;
        }
    }

    private void SetErrors(FluentValidation.Results.ValidationResult validationResult)
    {
        _areErrorsExist = true;

        ClearErrors();

        foreach(var error in validationResult.Errors)
        {
            switch(error.PropertyName)
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
            }
        }
    }

    private void ClearErrors()
    {
        TitleError = string.Empty;
        AuthorError = string.Empty;
        GenreError = string.Empty;
        IsbnError = string.Empty;
        PublishYearError = string.Empty;
        QuantityInStockError = string.Empty;
        FatalError = string.Empty;
    }
}
