using Autofac;
using Library.Helper;
using Library.Model.Configuration;
using Library.MongoRepository;
using Library.MongoRepository.Interface;
using Library.MsSqlDataAccess;
using Library.MsSqlRepository;
using Library.MsSqlRepository.Interface;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace WebService.MailService.IoC
{
    public class DependencyRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigHelper configHelper = new ConfigHelper();
            //Server=myServerName,myPortNumber;Database=myDataBase;User Id=myUsername;Password=myPassword;
            var mssqlDbConfig = configHelper.GetConfiguration<MsSqlDbConfig>(ConfigSystemType.MsSqlDb);
            var mssqlDbConnectionString = $"Server={mssqlDbConfig.Hostname},{mssqlDbConfig.Port};Database={mssqlDbConfig.Database};User Id={mssqlDbConfig.User};Password={mssqlDbConfig.Password};";

            DbContextOptions<MssqlDbContext> dbOptions = new DbContextOptionsBuilder<MssqlDbContext>().UseSqlServer(mssqlDbConnectionString).Options;
            builder.RegisterType<MssqlDbContext>().WithParameters(new[] { new NamedParameter("options", dbOptions) });


            builder.RegisterType<CompanyAccountRepository>().As<ICompanyAccountRepository>();
            //builder.RegisterType<InFileRepository>().As<IInFileRepository>();
            //builder.RegisterType<OutFileRepository>().As<IOutFileRepository>();
            builder.RegisterType<InMailRepository>().As<IInMailRepository>();
            builder.RegisterType<OutMailRepository>().As<IOutMailRepository>();
            builder.RegisterType<UserAuthenticationRepository>().As<IUserAuthenticationRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();


            //var mongoDbConfig = configHelper.GetConfiguration<MongoDbConfig>(ConfigSystemType.MongoDb);
            //builder.RegisterType<MongoClient>().As<IMongoClient>().WithParameters(new[] { new NamedParameter("connectionString", mongoDbConfig.HostUrl) });

            //builder.RegisterType<InFileDocumentRepository>().As<IInFileDocumentRepository>().WithParameters(new[] { new NamedParameter("dbName", mongoDbConfig.Database) });
            //builder.RegisterType<OutFileDocumentRepository>().As<IOutFileDocumentRepository>().WithParameters(new[] { new NamedParameter("dbName", mongoDbConfig.Database) });

            var rabitMqConfig = configHelper.GetConfiguration<RabbitMqConfig>(ConfigSystemType.RabbitMq);
            var busControl = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.Host(rabitMqConfig.Host, rabitMqConfig.Port, "MailVirtualHost",
                    s =>
                    {
                        s.Username(rabitMqConfig.User);
                        s.Password(rabitMqConfig.PassWord);

                    });
            });

            builder.AddMassTransit(s => s.AddBus(q => busControl));



        }
    }
}
