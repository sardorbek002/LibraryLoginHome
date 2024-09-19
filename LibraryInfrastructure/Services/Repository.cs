using CrudforMedicshop.Application.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace Library.Infrastructure.Services
{
    public class Repository : IRepository<User>
    {
        private readonly IdentityDbContext _context;

        public Repository(IdentityDbContext Mydbcontext)
        {
            _context = Mydbcontext;
        }

        public async Task<User> create(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public async Task<bool> delete(int deleteid)
        {
            var deletedobject = await _context.Users.FindAsync(deleteid);
            if (deletedobject != null)
            {
                _context.Users.Remove(deletedobject);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<User>> Getall()
        {
            var getall = _context.Users;
            if (getall != null)
            {
                return getall;
            }
            else
            {
                return null;
            }
        }

        public Task<User> getbyid(int id)
        {
            var getbyid = _context.Users.FirstAsync(x => x.Id == id);
            if (getbyid != null)
            {
                return getbyid;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<User>> SearchbyText(string MedicineName)
        {
            var Searchbytext = _context.Users.Select(x => x).Where(x => x.Name.Contains(MedicineName));
            return Searchbytext;

        }



        public async Task<bool> update(User entity)
        {
            var updatedobject = await _context.Users.FindAsync(entity.Id);
            if (updatedobject != null)
            {
                updatedobject.UserName = entity.UserName;
                updatedobject.Name = entity.Name;
                updatedobject.Phone = entity.Phone;
                updatedobject.Password = entity.Password;

                _context.Users.Update(updatedobject);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
