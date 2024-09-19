using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Permission
    {
        public int id { get; set; }
        public string name { get; set; }
        public ICollection<Role> roles { get; set; }
    }
}
