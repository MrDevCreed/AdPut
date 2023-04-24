using Data.Repositories.Common;
using Domain;
using System.Collections.Generic;

namespace Data.Repositories.Interfaces
{
    public interface ICityRepository : IRepositoryBase<City>
    {
        List<City> GetList();

        bool IsNameExists(string name);
    }
}
