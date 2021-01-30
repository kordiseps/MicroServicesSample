using Library.MsSqlDataAccess;
using Library.MsSqlDataAccess.Entity;
using Library.MsSqlRepository.Base;
using Library.MsSqlRepository.Interface;

namespace Library.MsSqlRepository
{
    public class UserAuthenticationRepository : GenericRepository<UserAuthentication>, IUserAuthenticationRepository
    {
        public UserAuthenticationRepository(MssqlDbContext Context) : base(Context) { }
    }
}
