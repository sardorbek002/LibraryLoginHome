using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Persistance
{
    public class IdentityDbContext:DbContext
    {
        public IdentityDbContext()
        {
            
        }
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            :base(options)
        {
            
        }
        public DbSet<User>Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> Roles { get; set; } 
        public DbSet<Permission> Permissions { get; set; }
       // public DbSet<Book> Books { get; set; }

    }
}
