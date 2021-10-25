using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IRepository<T> where T : Entidad
    {
        IEnumerable<T> GetList();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteByIdAsync(int id);
    }
}
