using Data.Database;
using Data.Repositories.Common;
using Data.Repositories.Interfaces;
using Domain;
using System.Linq;

namespace Data.Repositories.implementations
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context) : base(context)
        {
            this._context = context;
        }

        public User GetUserByUserId(string userId)
        {
            return _context.AppUsers.Where(P => P.UserId == userId).FirstOrDefault();
        }
    }
}
