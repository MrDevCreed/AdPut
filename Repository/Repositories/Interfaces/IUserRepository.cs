using Data.Repositories.Common;
using Domain;

namespace Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUserByUserId(string userId);
    }
}
