using Data.Repositories.Common;
using Domain;
using System.Collections.Generic;

namespace Data.Repositories.Interfaces
{
    public interface ITownRepository : IRepositoryBase<Town>
    {
        List<Town> GetList();

        List<Town> GetListByCity(int cityId);
    }
}
