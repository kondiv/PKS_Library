using Avalonia.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.Models;
using PKS_Library.Repositories.Interfaces;
using PKS_Library.Services.Interfaces;
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
        private readonly IBookService _bookService;

        private const int PageSize = 10;

        [ObservableProperty]
        private int _currentPage = 1;

        [ObservableProperty]
        private int _totalPages = 1;

        public ObservableCollection<Book> Books { get; private set; } = [];

        public AllBooksViewModel(IBookService bookService)
        {
            PageName = Data.PageName.Books;
            _bookService = bookService;
            LoadBooksAsync().ConfigureAwait(false);
        }

        [RelayCommand]
        private async Task LoadBooksAsync()
        {
            var allBooks = await _bookService.GetAllBooksAsync();

            TotalPages = allBooks.Count() / PageSize;

            var 

            foreach (var book in allBooks)
            {
                Books.Add(book);
            }
        }
    }
}
