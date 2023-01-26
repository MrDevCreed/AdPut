using Data.Database;
using Data.Repositories.Common;
using Data.Repositories.Interfaces;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories.implementations
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        private readonly Context _context;
        public CityRepository(Context context) : base(context)
        {
            this._context = context;
        }

        public List<City> GetList()
        {
            return _context.Cities.ToList();
        }
    }
}
