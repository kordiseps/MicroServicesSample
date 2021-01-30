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

namespace WindowsService.MailProcess3
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
                configure.SetServiceName("MailProcess3");
                configure.SetDisplayName("Mail Process 3 Service");
                configure.SetDescription("Mail Process 3 Service Açıklaması");

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
            Console.WriteLine("MailProcess3 started.");
        }
        public void Stop()
        {
            bus.Stop();
            Console.WriteLine("MailProcess3 Stopped.");
        }
    }

    public class EventConsumer : IConsumer<IMailProcess2Finished>
    {
        private IOutMailRepository outMailRepository;
        private ICompanyAccountRepository companyAccountRepository;
        public EventConsumer()
        {
            var container = IoC.GetIoC().Container;
            outMailRepository = container.Resolve<IOutMailRepository>();
            companyAccountRepository = container.Resolve<ICompanyAccountRepository>();
        }
        public Task Consume(ConsumeContext<IMailProcess2Finished> context)
        {
            Console.WriteLine($"Have new message. Process started." + context.Message.MailID);
            //Console.WriteLine("context.Message.CompanyId " + context.Message.CompanyId);
            //Console.WriteLine("context.Message.CompanyId " + context.Message.MailID);
            OutMail outMail = outMailRepository.GetById(Convert.ToInt32(context.Message.MailID));
            outMail.State = (int)MailState.Delivered;
            outMailRepository.Update(outMail);
            CompanyAccount companyAccount = companyAccountRepository.GetById(Convert.ToInt32(context.Message.CompanyId));
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("MailProcess3 İşlemi Tamamlandı" + context.Message.MailID);

            return Task.CompletedTask;
        }
    }
}
