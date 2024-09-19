using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CrudforMedicshop.Application.mapping
{
    public static class Addmapservice
    {
        public static void addmapping(this IServiceCollection serviceDescriptors)
        {
              serviceDescriptors.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}

