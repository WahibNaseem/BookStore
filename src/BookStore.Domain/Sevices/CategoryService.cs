using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Sevices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _cateRepo;
        private readonly IBookService _bookService;

        public CategoryService(ICategoryRepository cateRepo, IBookService bookService)
        {
            _cateRepo=cateRepo;
            _bookService=bookService;
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
            var categoryList =  await _cateRepo.GetAll();
            return  categoryList;
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _cateRepo.GetById(id);
            return category;
        }
        public async Task<Category> Add(Category category)
        {
            if(_cateRepo.Search(c => c.Name == category.Name).Result.Any())
                return null;

            await _cateRepo.Add(category);
            return category;
        }
        public async Task<Category> Update(Category category)
        {
            if(_cateRepo.Search(c => c.Name == category.Name && c.Id == category.Id).Result.Any())
                return null;

            await _cateRepo.Update(category);
            return category;
        }

        public async Task<bool> Remove(Category category)
        {
            var books = await _bookService.GetBookByCategory(category.Id);
            if(books.Any()) return false;

            await _cateRepo.Remove(category);
            return true;

        }

        public Task<IEnumerable<Category>> Search(string categoryName)
        {
            var categorySearchList = _cateRepo.Search(c => c.Name.Contains(categoryName));
            return categorySearchList;
        }     

            public void Dispose()
            {
            _cateRepo?.Dispose();
            }
        }
}
