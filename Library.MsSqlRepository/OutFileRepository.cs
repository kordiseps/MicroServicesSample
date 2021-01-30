using Library.MsSqlDataAccess;
using Library.MsSqlDataAccess.Entity;
using Library.MsSqlRepository.Base;
using Library.MsSqlRepository.Interface;

namespace Library.MsSqlRepository
{
    public class OutFileRepository : GenericRepository<OutFile>, IOutFileRepository
    {
        public OutFileRepository(MssqlDbContext Context) : base(Context) { }
    }
}
