using Library.Model.Api;
using Library.MsSqlDataAccess.Entity;
using Library.MsSqlRepository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;

namespace WebService.AuthService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IUserAuthenticationRepository userAuthenticationRepository;

        public AuthController(IUserRepository userRepository, IUserAuthenticationRepository userAuthenticationRepository)
        {
            this.userRepository = userRepository;
            this.userAuthenticationRepository = userAuthenticationRepository;
        }
        [HttpPost]
        public GetAuthKeyResponse GetAuthKey(GetAuthKeyRequest request)
        {
            var user = userRepository.GetFirstOrDefault(x => x.UserName == request.UserName && x.Password == OneWayEncrypt(request.Password));
            if (user is null)
            {
                return new GetAuthKeyResponse
                {
                    Completed = false,
                    AuthKey = string.Empty,
                    Code = "-1",
                    ExpireAt = null,
                    Message = "User Not Found"
                };
            }

            UserAuthentication auth = new UserAuthentication
            {
                AuthKey = Guid.NewGuid().ToString().ToLower(),
                ExpireAt = DateTime.Now.AddMinutes(180),
                UserId = user.UserId,
                InsertAt = DateTime.Now
            };
            userAuthenticationRepository.Create(auth);

            return new GetAuthKeyResponse
            {
                AuthKey = auth.AuthKey,
                Completed = true,
                Code = "1",
                ExpireAt = auth.ExpireAt.ToString("yyyy-MM-dd HH:mm:ss"),
                Message = "Success!"
            };
        }

        string OneWayEncrypt(string plainText)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            return Convert.ToBase64String(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainText)));
        }

    }

}
