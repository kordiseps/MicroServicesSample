using Library.Model.Api;
using Library.Model.Event;
using Library.Model.State;
using Library.MongoDocumentModel;
using Library.MongoRepository.Interface;
using Library.MsSqlRepository.Interface;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.FileService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IUserAuthenticationRepository userAuthenticationRepository;
        private readonly ICompanyAccountRepository companyAccountRepository;
        private readonly IInFileRepository inFileRepository;
        private readonly IOutFileRepository outFileRepository;
        private readonly IOutFileDocumentRepository outFileDocumentRepository;
        private readonly IInFileDocumentRepository inFileDocumentRepository;
        private readonly IBus bus;

        public FileController(
            IUserRepository userRepository,
            IUserAuthenticationRepository userAuthenticationRepository,
            ICompanyAccountRepository companyAccountRepository,
            IOutFileRepository outFileRepository,
            IInFileRepository inFileRepository,
            IOutFileDocumentRepository outFileDocumentRepository,
            IInFileDocumentRepository inFileDocumentRepository,
            IBus bus)
        {
            this.userRepository = userRepository;
            this.userAuthenticationRepository = userAuthenticationRepository;
            this.companyAccountRepository = companyAccountRepository;
            this.inFileRepository = inFileRepository;
            this.outFileDocumentRepository = outFileDocumentRepository;
            this.inFileDocumentRepository = inFileDocumentRepository;
            this.bus = bus;
            this.outFileRepository = outFileRepository;
        }

        [HttpPost]
        public SendFileResponse SendFile(SendFileRequest request)
        {
            var userAuthentication = userAuthenticationRepository.GetById(request.AuthKey);
            if (userAuthentication is null || userAuthentication.ExpireAt < DateTime.Now)
            {
                return new SendFileResponse
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
                return new SendFileResponse
                {
                    Code = "-1",
                    Completed = false,
                    Message = "Receiver Company Not Found!"
                };
            }

            var fileBytes = Convert.FromBase64String(request.FileBase64Content);
            var outFile = new OutFileDocumentModel
            {
                FileName = request.DocumentName,
                InsertTime = DateTime.Now,
                FileSize = fileBytes.Length
            };
            outFileDocumentRepository.Create(outFile, fileBytes);
            outFileRepository.Create(new Library.MsSqlDataAccess.Entity.OutFile
            {
                CompanyId = user.CompanyId,
                DocumentName = request.DocumentName,
                InsertAt = DateTime.Now,
                OutFileId = outFile.Id.ToString(),
                ReceiverIdentifier = request.ReceiverIdentifier,
                State = (int)FileState.Received
            });
            bus.Publish<IFileReceived>(new
            {
                CompanyId = user.CompanyId,
                FileID = outFile.Id.ToString()
            });


            return new SendFileResponse
            {
                Completed = true,
                Code = "1",
                Message = "File Received"
            };
        }
    }

}
