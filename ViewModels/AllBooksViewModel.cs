using Avalonia.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.CustomExceptions;
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

namespace PKS_Library.ViewModels
{
    public partial class AllBooksViewModel : PageViewModel
    {
        private NavigationService _navigationService;

        private readonly PageViewModelFactory _pageFactory;

        private readonly IBookService _bookService;

        public ObservableCollection<Book> Books { get; private set; } = [];

        public AllBooksViewModel(IBookService bookService, PageViewModelFactory pageFactory, NavigationService navigationService)
        {
            PageName = Data.PageName.Books;

            _bookService       = bookService;
            _pageFactory       = pageFactory;
            _navigationService = navigationService;

           LoadBooksAsync().ConfigureAwait(false);
        }

        private async Task LoadBooksAsync()
        {
            var allBooks = await _bookService.GetAllBooksAsync();

            foreach (var book in allBooks)
            {
                Books.Add(book);
            }
        }

        [RelayCommand]
        public void OpenEditBookPage(Book book)
        {
            var editPage = _pageFactory.GetPageViewModel(Data.PageName.BookEdit) as BookEditViewModel ?? 
                throw new PageDoesNotExistException("Не удалось открыть страницу редактирования книги");

            editPage.SetBook(book);

            _navigationService.NavigateTo(editPage);
        }
    }
}
