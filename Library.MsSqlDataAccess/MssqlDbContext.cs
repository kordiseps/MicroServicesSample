using Library.MsSqlDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace Library.MsSqlDataAccess
{
    public class MssqlDbContext : DbContext
    {
        public MssqlDbContext(DbContextOptions<MssqlDbContext> options) : base(options) { }
        public DbSet<CompanyAccount> CompanyAccount { get; set; }
        public DbSet<InFile> InFile { get; set; }
        public DbSet<InMail> InMail { get; set; }
        public DbSet<OutFile> OutFile { get; set; }
        public DbSet<OutMail> OutMail { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserAuthentication> UserAuthentication { get; set; }
    }
}
