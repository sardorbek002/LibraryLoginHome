using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Libraray.Application.Mapping
{
    public class addmap : Profile
    {
        public addmap()
        {          
            CreateMap<RoleCreateDTO, Role>().ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.userids.Select(userid => new User { Id = userid })))
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissionids.Select(permissionid => new Permission { id = permissionid })));
        }
    }
}
