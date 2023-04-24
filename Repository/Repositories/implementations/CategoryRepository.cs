using Data.Repositories.Common;
using Domain;
using Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.implementations
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly Context _context;
        public CategoryRepository(Context context) : base(context)
        {
            this._context = context;
        }

        public List<Category> GetBaseList()
        {
            return _context.Categories.Where(P => P.IsBaseCategory).Include(P => P.SubCategories).ToList();
        }

        public List<Category> GetList()
        {
            return _context.Categories.ToList();
        }

        public bool IsTitleExists(string name)
        {
            return _context.Categories.Any(P => P.Title == name);
        }
    }
}
