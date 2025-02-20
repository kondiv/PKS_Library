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

        public ObservableCollection<Book> Books { get; private set; } = [];

        public AllBooksViewModel(IBookService bookService)
        {
            PageName = Data.PageName.Books;
            _bookService = bookService;
            LoadDataAsync().ConfigureAwait(false);
        }

        private async Task LoadDataAsync()
        {
            var books = await _bookService.GetAllBooksAsync();

            foreach (var book in books)
            {
                Books.Add(book);
            }
        }
    }
}
