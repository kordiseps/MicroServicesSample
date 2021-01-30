using Library.MsSqlDataAccess;
using Library.MsSqlDataAccess.Entity;
using Library.MsSqlRepository.Base;
using Library.MsSqlRepository.Interface;

namespace Library.MsSqlRepository
{
    public class InFileRepository : GenericRepository<InFile>, IInFileRepository
    {
        public InFileRepository(MssqlDbContext Context) : base(Context) { }
    }
}
