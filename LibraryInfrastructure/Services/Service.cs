using CrudforMedicshop.Application.Interfaces;
using Libraray.Application.Services;
using Library.Domain.Entities;

namespace CrudforMedicshop.infrastructure.Services
{
    public class Service : Iservice<User>
    {
        private readonly IRepository<User> _repostory;
        public Service(IRepository<User> repostory)
        {
            _repostory = repostory;
        }

        public async Task<User> Create(User entity)
        {
            await _repostory.create(entity);
            return entity;
        }

        public async Task<string> Delete(int deletedid)
        {
            if (await _repostory.delete(deletedid) == true)
            {
                return "object is deleted";
            }
            return "object is not deleted";
        }

        public async Task<IEnumerable<User>> Getall()
        {
            IEnumerable<User> getall = await _repostory.Getall();
            return getall;
        }


        public async Task<User> GetById(int id)
        {
            return await _repostory.getbyid(id);
        }

        public async Task<IEnumerable<User>> SearchbyText(string MedicineName)
        {
            return await _repostory.SearchbyText(MedicineName);
        }

        public async Task<string> Update(User entity)
        {
            if (await _repostory.update(entity))
            {
                return "updated";
            }
            return "error";
        }
    }
}
