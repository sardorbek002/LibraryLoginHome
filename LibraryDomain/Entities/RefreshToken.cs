using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public string RefreshTokenValue { get; set; }
        public DateTime ExpiretTime { get; set; }
    }
}
