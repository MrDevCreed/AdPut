using Data.Repositories.Common;
using Domain;
using System.Collections.Generic;

namespace Data.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        List<Category> GetList();
    }
}
