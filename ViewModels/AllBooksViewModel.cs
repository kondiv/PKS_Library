using PKS_Library.Models;
using PKS_Library.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.ViewModels
{
    public partial class AllBooksViewModel
    {
        private readonly IBookRepository _bookRepository;

        public ObservableCollection<Book> Books { get; private set; } = [];

        public AllBooksViewModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;

        }

        private async Task LoadDataAsync()
        {
            var books = await _bookRepository.GetAllBooks();

            foreach (var book in books)
            {
                Books.Add(book);
            }
        }
    }
}
