using Library.MsSqlDataAccess;
using Library.MsSqlDataAccess.Entity;
using Library.MsSqlRepository.Base;
using Library.MsSqlRepository.Interface;

namespace Library.MsSqlRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MssqlDbContext Context) : base(Context) { }
    }
}
