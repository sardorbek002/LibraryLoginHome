using AutoMapper;
using Libraray.Application.Services;
using Library.Domain.Entities;
using Library.Domain.Models;
using Library.Infrastructure.Persistance;
using Microsoft.Extensions.Configuration;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IdentityDbContext _dbcontext;
        private readonly IConfiguration _configuration;

        public RoleService(IdentityDbContext dbcontext, IConfiguration configuration, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Response<Role>> CreateRole(RoleCreateDTO roleDTO)
        {
            var Role = _dbcontext.Roles.Where(x => x.Name == roleDTO.Name).FirstOrDefault();
            if (Role != null)
            {
                return new("Role is already Created");
            }
            var role = _mapper.Map<Role>(roleDTO);
            _dbcontext.Roles.Add(role);
            var rows = _dbcontext.SaveChanges();
            if (rows > 0)
            {


                return new(role);
            }
            return new("something went wrong");

        }

        public async Task<bool> DeleteRole(int Roleid)
        {
            var Role = _dbcontext.Roles.Where(x => x.Id == Roleid).FirstOrDefault();
            _dbcontext.Roles.Remove(Role);
            var rows = _dbcontext.SaveChanges();
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            var getall = _dbcontext.Roles;
            return getall;
        }

        public async Task<Role> GetbyidRole(int Roleid)
        {
            return _dbcontext.Roles.Select(x => x).Where(x => x.Id == Roleid).First();
        }

        public async Task<bool> UpdateRole(RoleCreateDTO roleDTO)
        {
            var Role1 = _dbcontext.Roles.Select(x => x).Where(x => x.Id == roleDTO.Id).First();
            var Role = _mapper.Map<Role>(roleDTO);
            if (Role != null)
            {
                _dbcontext.Roles.Update(Role);
                _dbcontext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
