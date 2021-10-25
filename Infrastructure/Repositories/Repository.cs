using Common.Interfaces;
using Common.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entidad
    {
        private readonly PrimeStoneDataBaseContext _dataBaseContext;
        protected DbSet<T> _entities;
        public Repository(PrimeStoneDataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
            _entities = _dataBaseContext.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            entity.EstaBorrado = false;
            entity.FechaCreacion = DateTime.Now;
            await _entities.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            T entity = await GetByIdAsync(id);
            entity.EstaBorrado = true;
            entity.FechaBorrado = DateTime.Now;
            _entities.Update(entity);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id && item.EstaBorrado == false);
        }

        public IEnumerable<T> GetList()
        {
            return _entities.Where(item => item.EstaBorrado == false).AsEnumerable();
        }

        public void Update(T entity)
        {
            entity.FechaActualizacion = DateTime.Now;            
            _entities.Update(entity);
        }
    }
}