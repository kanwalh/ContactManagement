using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Services
{
    public interface IRepo<T>
    {

        public IEnumerable<T> GetAll();
        public T GetById(int id);

        public T Create(T entity);
        public Task<T> CreateAsync(T entity);

        public T Update(T entity);
        public Task<T> UpdateAsync(T entity);

        public void Delete(T entity);
        public void DeleteById(int id);

        public Task DeleteByIdAsync(int id);
    }
}
