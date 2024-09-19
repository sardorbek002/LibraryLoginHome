using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraray.Application.Services;

public interface Iservice<T> where T : class
{
    public Task<T> Create(T entity);
    public Task<string> Update(T entity);
    public Task<string> Delete(int deletedid);
    public Task<IEnumerable<T>> Getall();
    public Task<T> GetById(int id);
    public Task<IEnumerable<User>> SearchbyText(String UserName);
}
