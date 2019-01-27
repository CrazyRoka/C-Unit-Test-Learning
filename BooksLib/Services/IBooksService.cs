using BooksLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksLib.Services
{
    public interface IBooksService
    {
        Task<Book> AddOrUpdateBookAsyns(Book book);
        Book GetBook(int bookId);
        Task LoadBooksAsyns();

        IEnumerable<Book> Books { get; }
    }
}
