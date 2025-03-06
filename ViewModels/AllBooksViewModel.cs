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

        [ObservableProperty]
        private string _infoMessage = string.Empty;

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

            Books = new ObservableCollection<Book>(allBooks);
        }

        [RelayCommand]
        public void OpenEditBookPage(Book book)
        {
            var editPage = _pageFactory.GetPageViewModel(Data.PageName.BookEdit) as BookEditViewModel ?? 
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
    }
}
