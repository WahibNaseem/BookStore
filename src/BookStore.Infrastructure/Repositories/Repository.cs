using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly BookStoreDbContext BookStoreDb;

        protected readonly DbSet<TEntity> DbSet;

        protected Repository(BookStoreDbContext bookStoreDb)
        {
            BookStoreDb=bookStoreDb;
            DbSet = bookStoreDb.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            var list = await DbSet.ToListAsync();
            return list;
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            var entity = await DbSet.FindAsync(id);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            var searchResult = await DbSet.AsNoTracking().Where(predicate).ToListAsync();
            return searchResult;
        }


        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            var isSave = await BookStoreDb.SaveChangesAsync();
            return isSave;
        }

        public void Dispose()
        {
            BookStoreDb?.Dispose();
        }
    }
}
