using Data.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Repositories.Common
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly Context _context;

        protected DbSet<T> _dbSet;

        public RepositoryBase(Context context)
        {
            this._context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T Entity)
        {
            _dbSet.Add(Entity);
        }

        public void Delete(T Entity)
        {
            _dbSet.Remove(Entity);
        }

        public void Edit(T Entity)
        {
            _dbSet.Update(Entity);
        }

        public T Find(T Entity)
        {
            return _dbSet.Find(Entity);
        }

        public T FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
