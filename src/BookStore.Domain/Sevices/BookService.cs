using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Sevices
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository=bookRepository;
        }
        public async Task<Book> Add(Book book)
        {
            if(_bookRepository.Search(b => b.Name == book.Name).Result.Any())
                return null;

            await _bookRepository.Add(book);
            return book;

        }

        public void Dispose()
        {
            _bookRepository?.Dispose();
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var books = await _bookRepository.GetAll();
            return books;
        }

        public async Task<IEnumerable<Book>> GetBookByCategory(int categoryId)
        {
            var bookCategory = await _bookRepository.GetBooksByCategory(categoryId);
            return bookCategory;
        }

        public async Task<Book> GetById(int id)
        {
            var book = await _bookRepository.GetById(id);
            return book;
        }

        public async Task<bool> Remove(Book book)
        {
            await _bookRepository.Remove(book);
            return true;
        }

        public async Task<IEnumerable<Book>> Search(string bookName)
        {
            var searchResult = await _bookRepository.Search(s => s.Name.Contains(bookName));
            return searchResult;
        }

        public async Task<IEnumerable<Book>> SearchBookWithCategory(string searchValue)
        {
            var bookwithCategorySearch = await _bookRepository.SearchBookWithCategory(searchValue);
            return bookwithCategorySearch;
        }

        public async Task<Book> Update(Book book)
        {
            if(_bookRepository.Search(s => s.Name == book.Name && s.Id == book.Id).Result.Any())
                return null;

            await _bookRepository.Update(book);
            return book;
        }
    }
}
