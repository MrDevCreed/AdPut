using Data.Database;
using Data.Repositories.Common;
using Data.Repositories.Interfaces;
using Domain;
using System;
using System.Linq;

namespace Data.Repositories.implementations
{
    public class AdRepository : RepositoryBase<Ad>, IAdRepository
    {
        private readonly Context _context;
        public AdRepository(Context context) : base(context)
        {
            this._context = context;
        }

        public PageCollection<Ad> GetFilteredList(int pageSize,
                                                  int pageNumber,
                                                  string name,
                                                  int? minPrice,
                                                  int? maxPrice,
                                                  int? categoryId,
                                                  int? addressId,
                                                  int? userId,
                                                  AdStatus? adStatus,
                                                  bool? onlyWithPhoto)
        {
            IQueryable<Ad> query = _context.Ads;

            if(!String.IsNullOrWhiteSpace(name))
                query = query.Where(P => P.Name.Contains(name));

            if(minPrice.HasValue && minPrice != 0)
                query = query.Where(P => P.Price >= minPrice);

            if(maxPrice.HasValue && maxPrice != 0)
                query = query.Where(P => P.Price <= maxPrice);

            if(categoryId.HasValue && categoryId != 0)
                query = query.Where(P => P.Category.Id == categoryId);

            if(addressId.HasValue && addressId != 0)
                query = query.Where(P => P.Address.Id == addressId);

            if (userId.HasValue && userId != 0)
                query = query.Where(P => P.User.Id == userId);

            if (adStatus.HasValue)
                query = query.Where(P => P.AdStatus == adStatus);

            if (onlyWithPhoto == true)
                query = query.Where(P => P.Images.Count != 0);

            int recordCount = query.Count();
            int remainder = 0;
            int pageCount = Math.DivRem(recordCount, pageSize, out remainder);
            if (remainder != 0)
            {
                pageCount++;
            }

            PageCollection<Ad> adPageCollection = new PageCollection<Ad>()
            {
                PageCount = pageCount,
                PageSize = pageSize,
                RecordCount = recordCount,
                PageNumber = pageNumber,
                Data = query.Skip((pageNumber * pageSize) - pageSize).Take(pageSize).ToList(),
            };

            return adPageCollection;
        }
    }
}
