using Data.Database;
using Data.Repositories.Common;
using Data.Repositories.Interfaces;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories.implementations
{
    public class TownRepository : RepositoryBase<Town> , ITownRepository
    {
        private readonly Context _context;
        public TownRepository(Context context) : base(context)
        {
            this._context = context;
        }

        public List<Town> GetListByCity(int cityId)
        {
            return _context.Towns.Where(P => P.City.Id == cityId).ToList();
        }

        public List<Town> GetList()
        {
            return _context.Towns.ToList();
        }
    }
}
