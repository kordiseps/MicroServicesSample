using Library.MsSqlDataAccess;
using Library.MsSqlDataAccess.Entity;
using Library.MsSqlRepository.Base;
using Library.MsSqlRepository.Interface;

namespace Library.MsSqlRepository
{
    public class CompanyAccountRepository : GenericRepository<CompanyAccount>, ICompanyAccountRepository
    {
        public CompanyAccountRepository(MssqlDbContext Context) : base(Context) { }
    }
}
