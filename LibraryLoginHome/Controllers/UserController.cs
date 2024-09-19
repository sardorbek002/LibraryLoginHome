using AutoMapper;
using CrudforMedicshop.infrastructure.Services;
using Libraray.Application.Services;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CrudforMedicshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Iservice<User> _iservice;
        private readonly IMapper _mapper;

        public AccountController(Iservice<User> service, IMapper mapper)
        {
            _iservice = service;
            _mapper = mapper;
        }
        [HttpGet("GetAllUser")]
        [AuthefrationAttributeFilter("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _iservice.Getall());
        }

        [HttpGet("GetbyidUser")]
        [AuthefrationAttributeFilter("GetbyidUser")]
        public async Task<IActionResult> GetbyidUser(int id)
        {
            var user = await _iservice.GetById(id);
            if (user != null)
            {
                return Ok(user);    
            }
            return NotFound("User not found");
        }

        [HttpPost("CreateUser")]
        [AuthefrationAttributeFilter("CreateUser")]
        public async Task<IActionResult> CreateUser(User user)
        {
                User user1 = _mapper.Map<User>(User);

            var result = await _iservice.Create(user1);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        [AuthefrationAttributeFilter("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _iservice.Delete(id);
            return Ok(result);
        }

        [HttpPatch("Update")]
        [AuthefrationAttributeFilter("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var result = await _iservice.Update(user);
            return Ok(result);
        }
 
        [HttpGet("SearchbyText")]
        [AuthefrationAttributeFilter("SearchbyText")]
        public async Task<IActionResult> SearchbyText(string user )
        {
            var result = await _iservice.SearchbyText(user);
            return Ok(result);
        }
        
    }
}
