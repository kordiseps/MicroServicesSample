using Autofac;
using Library.Model.Event;
using Library.Model.State;
using Library.MsSqlDataAccess.Entity;
using Library.MsSqlRepository;
using Library.MsSqlRepository.Interface;
using MassTransit;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Autofac;

namespace WindowsService.MailProcess2
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
                configure.SetServiceName("MailProcess2");
                configure.SetDisplayName("Mail Process 2 Service");
                configure.SetDescription("Mail Process 2 Service Açıklaması");

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
            Console.WriteLine("MailProcess2 started.");
        }
        public void Stop()
        {
            bus.Stop();
            Console.WriteLine("MailProcess2 Stopped.");
        }
    }

    public class EventConsumer : IConsumer<IMailProcess1Finished>
    {
        private IOutMailRepository outMailRepository;
        private ICompanyAccountRepository companyAccountRepository;
        private IBus bus;
        public EventConsumer()
        {
            var container = IoC.GetIoC().Container;
            outMailRepository = container.Resolve<IOutMailRepository>();
            companyAccountRepository = container.Resolve<ICompanyAccountRepository>();
            bus = container.Resolve<IBus>();
        }
        public Task Consume(ConsumeContext<IMailProcess1Finished> context)
        {
            Console.WriteLine($"Have new message. Process started.");
            Console.WriteLine("context.Message.CompanyId " + context.Message.CompanyId);
            Console.WriteLine("context.Message.CompanyId " + context.Message.MailID);
            OutMail outMail = outMailRepository.GetById(Convert.ToInt32(context.Message.MailID));
            outMail.State = (int)MailState.Processed2;
            outMailRepository.Update(outMail);
            CompanyAccount companyAccount = companyAccountRepository.GetById(Convert.ToInt32(context.Message.CompanyId));
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("MailProcess2 İşlemi Tamamlandı");

            bus.Publish<IMailProcess2Finished>(new
            {
                CompanyId = context.Message.CompanyId,
                MailID = context.Message.MailID
            });
            return Task.CompletedTask;
        }
    }
}
