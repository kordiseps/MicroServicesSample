using Library.MsSqlDataAccess;
using Library.MsSqlDataAccess.Entity;
using Library.MsSqlRepository.Base;
using Library.MsSqlRepository.Interface;

namespace Library.MsSqlRepository
{
    public class OutMailRepository : GenericRepository<OutMail>, IOutMailRepository
    {
        public OutMailRepository(MssqlDbContext Context) : base(Context) { }
    }
}
