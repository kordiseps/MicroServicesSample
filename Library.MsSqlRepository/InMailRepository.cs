using Library.MsSqlDataAccess;
using Library.MsSqlDataAccess.Entity;
using Library.MsSqlRepository.Base;
using Library.MsSqlRepository.Interface;

namespace Library.MsSqlRepository
{
    public class InMailRepository : GenericRepository<InMail>, IInMailRepository
    {
        public InMailRepository(MssqlDbContext Context) : base(Context) { }
    }
}
