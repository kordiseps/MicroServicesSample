using Library.Model.Api;
using Library.Model.Event;
using Library.Model.State;
using Library.MsSqlDataAccess.Entity;
using Library.MsSqlRepository.Interface;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.MailService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IUserAuthenticationRepository userAuthenticationRepository;
        private readonly ICompanyAccountRepository companyAccountRepository;
        private readonly IOutMailRepository outMailRepository;
        private readonly IInMailRepository inMailRepository;
        private readonly IBus bus;

        public MailController(
            IUserRepository userRepository,
            IUserAuthenticationRepository userAuthenticationRepository,
            ICompanyAccountRepository companyAccountRepository,
            IOutMailRepository outMailRepository,
            IInMailRepository inMailRepository,
            IBus bus)
        {
            this.userRepository = userRepository;
            this.userAuthenticationRepository = userAuthenticationRepository;
            this.companyAccountRepository = companyAccountRepository;
            this.outMailRepository = outMailRepository;
            this.inMailRepository = inMailRepository;
            this.bus = bus;
        }

        [HttpPost]
        public SendMailResponse SendMail(SendMailRequest request)
        {
            var userAuthentication = userAuthenticationRepository.GetById(request.AuthKey);
            if (userAuthentication is null || userAuthentication.ExpireAt < DateTime.Now)
            {
                return new SendMailResponse
                {
                    Code = "-1",
                    Completed = false,
                    Message = "Not Authenticated"
                };
            }
            var user = userRepository.GetById(userAuthentication.UserId);
            var company = companyAccountRepository.GetFirstOrDefault(x => x.Identifier == request.ReceiverIdentifier);
            if (company is null)
            {
                return new SendMailResponse
                {
                    Code = "-1",
                    Completed = false,
                    Message = "Receiver Company Not Found!"
                };
            }

            var outMail = new OutMail
            {
                Body = request.Body,
                CompanyId = user.CompanyId,
                ReceiverIdentifier = request.ReceiverIdentifier,
                InsertAt = DateTime.Now,
                State = (int)MailState.Received,
                Subject = request.Subject,
            };

            outMailRepository.Create(outMail);


            bus.Publish<IMailReceived>(new
            {
                CompanyId = user.CompanyId,
                MailID = outMail.MailId.ToString()
            });


            return new SendMailResponse
            {
                Completed = true,
                Code = "1",
                Message = "Mail Received"
            };
        }
    }

}
