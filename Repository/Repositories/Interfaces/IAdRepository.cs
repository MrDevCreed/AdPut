using Data.Repositories.Common;
using Domain;

namespace Data.Repositories.Interfaces
{
    public interface IAdRepository : IRepositoryBase<Ad> 
    {
        PageCollection<Ad> GetFilteredList(int pageSize,
                                           int pageNumber,
                                           string name,
                                           int? minPrice,
                                           int? maxPrice,
                                           int? categoryId,
                                           int? addressId,
                                           int? userId,
                                           AdStatus? adStatus,
                                           bool? onlyWithPhoto);
    }
}
