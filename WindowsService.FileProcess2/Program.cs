using Autofac;
using Library.Model.Event;
using Library.Model.State;
using Library.MongoDocumentModel;
using Library.MongoRepository.Interface;
using Library.MsSqlDataAccess.Entity;
using Library.MsSqlRepository;
using Library.MsSqlRepository.Interface;
using MassTransit;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Autofac;

namespace WindowsService.FileProcess2
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(configure =>
            {
                IoC ioc = IoC.GetIoC();

                configure.UseAutofacContainer(ioc.Container);

                configure.Service<MyService>(service =>
                {
                    service.ConstructUsingAutofacContainer();
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });

                configure.RunAsLocalSystem();
                configure.SetServiceName("FileProcess2");
                configure.SetDisplayName("File Process 2 Service");
                configure.SetDescription("File Process 2 Service Açıklaması");

            });
        }
    }

    public class MyService
    {
        private readonly IBusControl bus;
        public MyService(IBusControl bus)
        {
            this.bus = bus;
        }
        public void Start()
        {
            bus.Start();
            Console.WriteLine("FileProcess2 started.");
        }
        public void Stop()
        {
            bus.Stop();
            Console.WriteLine("FileProcess2 Stopped.");
        }
    }

    public class EventConsumer : IConsumer<IFileProcess1Finished>
    {
        private IOutFileRepository outFileRepository;
        private IOutFileDocumentRepository outFileDocumentRepository;
        private ICompanyAccountRepository companyAccountRepository;
        private IBus bus;
        public EventConsumer()
        {
            var container = IoC.GetIoC().Container;
            outFileRepository = container.Resolve<IOutFileRepository>();
            companyAccountRepository = container.Resolve<ICompanyAccountRepository>();
            outFileDocumentRepository = container.Resolve<IOutFileDocumentRepository>();
            bus = container.Resolve<IBus>();
        }
        public Task Consume(ConsumeContext<IFileProcess1Finished> context)
        {
            Console.WriteLine($"Have new message. Process started.");
            Console.WriteLine("context.Message.CompanyId " + context.Message.CompanyId);
            Console.WriteLine("context.Message.CompanyId " + context.Message.FileID);
            OutFile outFile = outFileRepository.GetById(context.Message.FileID);
            outFile.State = (int)FileState.Processed2;
            outFileRepository.Update(outFile);
            OutFileDocumentModel outFileDocument = outFileDocumentRepository.Get(context.Message.FileID);
            var outFileBytes = outFileDocumentRepository.GetFile(outFileDocument.FileId);
            Console.WriteLine("File Size : " + outFileBytes.Length);
            CompanyAccount companyAccount = companyAccountRepository.GetById(Convert.ToInt32(context.Message.CompanyId));
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("FileProcess2 İşlemi Tamamlandı");
            bus.Publish<IFileProcess2Finished>(new
            {
                CompanyId = context.Message.CompanyId,
                FileID = context.Message.FileID
            });
            return Task.CompletedTask;
        }
    }
}
