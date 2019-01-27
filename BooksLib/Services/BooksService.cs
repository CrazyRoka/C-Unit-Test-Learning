using BooksLib.Models;
using BooksLib.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLib.Services
{
    public class BooksService : IBooksService
    {
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();
        private IBooksRepository _booksRepository;
        public BooksService(IBooksRepository repository) => _booksRepository = repository;

        public IEnumerable<Book> Books => _books;

        public async Task<Book> AddOrUpdateBookAsyns(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            Book updated = null;
            if(book.BookId == 0)
            {
                updated = await _booksRepository.AddAsync(book);
                if (updated == null) throw new InvalidOperationException();

                _books.Add(updated);
            }
            else
            {
                updated = await _booksRepository.UpdateAsync(book);
                if (updated == null) throw new InvalidOperationException();

                Book old = _books.Where(b => b.BookId == updated.BookId).Single();
                int index = _books.IndexOf(old);
                _books.RemoveAt(index);
                _books.Insert(index, updated);
            }
            return updated;
        }

        public Book GetBook(int bookId) => _books.Where(b => b.BookId == bookId).SingleOrDefault();

        public async Task LoadBooksAsyns()
        {
            if (_books.Count > 0) return;
            IEnumerable<Book> books = await _booksRepository.GetItemsAsync();
            _books.Clear();
            foreach(var book in books)
            {
                _books.Add(book);
            }
        }
    }
}
