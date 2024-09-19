using Library.Domain.Entities;

namespace CrudforMedicshop.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<T> create(T entity);
        public Task<bool> update(T entity);
        public Task<bool> delete(int deleteid);
        public Task<IEnumerable<T>> Getall();
        public Task<T> getbyid(int id);
        public Task<IEnumerable<User>> SearchbyText(String MedicineName);
    }
}
