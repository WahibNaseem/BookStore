using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BookStoreDbContext context) : base(context) { }


        public override async Task<List<Book>> GetAll()
        {
            var listOfBooks = await BookStoreDb.Books.AsNoTracking().Include(b => b.Category).ToListAsync();
            return listOfBooks;
        }

        public override async Task<Book> GetById(int id)
        {
            var book = await BookStoreDb.Books.AsNoTracking().Include(b => b.Category).Where(b => b.Id == id).FirstOrDefaultAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetBooksByCategory(int categoryId)
        {
            var booksWithCategory = await Search(s => s.CategoryId == categoryId);
            return booksWithCategory;
        }

        public async Task<IEnumerable<Book>> SearchBookWithCategory(string searchValue)
        {
            var searchBooksCategoryResult = await BookStoreDb.Books.AsNoTracking()
                                                                  .Include(b => b.Category).Where(b => b.Name.Contains(searchValue) ||
                                                                                                                        b.Author.Contains(searchValue) ||
                                                                                                                        b.Description.Contains(searchValue) ||
                                                                                                                        b.Category.Name.Contains(searchValue)).ToListAsync();
            return searchBooksCategoryResult;
        }
    }
}
