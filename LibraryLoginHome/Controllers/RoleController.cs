using Libraray.Application.Services;
using Library.Domain.Models;
using Library.Infrastructure.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace  LibraryLoginHome.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IdentityDbContext _dbcontext;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> getallRole()
        {
            return Ok(await _roleService.GetAllRoles());
        }
        [HttpGet]
        public async Task<IActionResult> GetbyidRoles(int Roleid)
        {
            return Ok(await _roleService.GetbyidRole(Roleid));
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateDTO roleCreateDTO)
        {
            var Role = _dbcontext.Roles.Where(x => x.Name == roleCreateDTO.Name).FirstOrDefault();
            if (Role == null)
            {
                await _roleService.CreateRole(roleCreateDTO);
            }
            return Ok("Role Already exist");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(int RoleId)
        {
            var Role = _dbcontext.Roles.Where(x => x.Id == RoleId).FirstOrDefault();
            if (Role == null)
            {
                await _roleService.DeleteRole(RoleId);
            }
            return Ok("Role not Found");
        }
        [HttpPatch]
        public async Task<IActionResult> Update(RoleCreateDTO role)
        {
            var Role = _dbcontext.Roles.Where(x => x.Id == role.Id).FirstOrDefault();
            if (Role == null)
            {
                await _roleService.UpdateRole(role);
            }
            return Ok("Role not Found");
        }
    }
}
