using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Models
{
    public class RoleCreateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<int>? userids { get; set; }
        public virtual ICollection<int>? Permissionids { get; set; }
    }
}
