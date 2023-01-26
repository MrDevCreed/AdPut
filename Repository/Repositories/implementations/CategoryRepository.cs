using Data.Repositories.Common;
using Domain;
using Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Data.Database;

namespace Data.Repositories.implementations
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly Context _context;
        public CategoryRepository(Context context) : base(context)
        {
            this._context = context;
        }

        public List<Category> GetList()
        {
            return _context.Categories.ToList();
        }
    }
}
