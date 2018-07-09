using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using ApiContent.DataAccess;
using ApiContent.Models;
using ApiContent.Services;

namespace ApiContent.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private readonly IUserData _userRepo = null;
        private readonly ICryptoService _cryptoService;

        public UsersController()
        {
            _userRepo = new UserData();
            _cryptoService = new CryptoService();
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            user.Password = _cryptoService.CryptPassword(user.Password);
            var id = await _userRepo.AddOrUpdateUser(user);
            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("")]
        public async Task<List<User>> GetUsers(string filter)
        {
            var users = await _userRepo.GetUsers(filter);
            return users;
        }
        
    }
}
