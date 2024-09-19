using Libraray.Application.Services;
using Library.Domain.Entities;
using Library.Domain.Entities.Models;
using Library.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace LibraryLoginHome.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpPost]
        public async Task<Response<(User,Token)>> Registr(User user)
            =>await _identityService.Register(user);
        [HttpPost]
        [Authorize]
        public async Task<Response<(User, Token)>> Login(Credential credential)
            => await _identityService.Login(credential);
        [HttpGet]
        public async Task<Response<bool>> Logout( )
            => await _identityService.Logout();
        [HttpDelete]
        [Authorize]
        public async Task<Response<bool>> DeleteUser(int id)
            => await _identityService.DeleteUserAsync(id);
    }
}
