using BooksLib.Models;
using BooksLib.Repositories;
using BooksLib.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace BooksLib.Tests.Services
{
    public class BooksServiceTest : IDisposable
    {
        private const string TestTitle = "Test Title";
        private const string UpdatedTestTitle = "Updated Test Title";
        public const string APublisher = "A Publisher";
        private BooksService _booksService;
        private Book _newBook = new Book
        {
            BookId = 0,
            Title = TestTitle,
            Publisher = APublisher
        };
        private Book _expectedBook = new Book
        {
            BookId = 1,
            Title = TestTitle,
            Publisher = APublisher
        };
        private Book _notInRepositoryBook = new Book
        {
            BookId = 42,
            Title = TestTitle,
            Publisher = APublisher
        };
        private Book _updatedBook = new Book
        {
            BookId = 1,
            Title = UpdatedTestTitle,
            Publisher = APublisher
        };

        public BooksServiceTest()
        {
            var mock = new Mock<IBooksRepository>();
            mock.Setup(repository => repository.AddAsync(_newBook)).ReturnsAsync(_expectedBook);
            mock.Setup(repository => repository.UpdateAsync(_notInRepositoryBook)).ReturnsAsync(null as Book);
            mock.Setup(repository => repository.UpdateAsync(_updatedBook)).ReturnsAsync(_updatedBook);

            _booksService = new BooksService(mock.Object);
        }

        [Fact]
        public async Task GetBook_ReturnsExistingBook()
        {
            await _booksService.AddOrUpdateBookAsyns(_newBook);
            Book actualBook = _booksService.GetBook(1);
            Assert.Equal(_expectedBook, actualBook);
        }

        [Fact]
        public void GetBook_ReturnsNullForNotExistingBook()
        {
            Book actualBook = _booksService.GetBook(123);
            Assert.Null(actualBook);
        }

        [Fact]
        public async Task AddOrUpdateBookAsyns_ThrowsForNull()
        {
            Book nullBook = null;
            await Assert.ThrowsAsync<ArgumentNullException>(() => _booksService.AddOrUpdateBookAsyns(nullBook));
        }

        [Fact]
        public async Task AddOrUpdateBookAsync_AddedBookReturnsFromRepository()
        {
            Book actualBook = await _booksService.AddOrUpdateBookAsyns(_newBook);
            Assert.Equal(_expectedBook, actualBook);
            Assert.Contains(actualBook, _booksService.Books);
        }

        [Fact]
        public async Task AddOrUpdateBookAsyns_UpdateBook()
        {
            await _booksService.AddOrUpdateBookAsyns(_newBook);
            Book updatedBook = await _booksService.AddOrUpdateBookAsyns(_updatedBook);
            Assert.Equal(_updatedBook, updatedBook);
            Assert.Contains(_updatedBook, _booksService.Books);
        }

        [Fact]
        public async Task AddOrUpdateBook_UpdateNotExistingBookThrows()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() => _booksService.AddOrUpdateBookAsyns(_notInRepositoryBook));
        }

        public void Dispose()
        {
        }
    }
}
