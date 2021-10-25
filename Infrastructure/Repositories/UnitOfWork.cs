using Common.Interfaces;
using Common.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PrimeStoneDataBaseContext _dataBaseContext;
        private readonly IRepository<Curso> _cursoRepository;
        private readonly IRepository<Direccion> _direccionRepository;
        private readonly IRepository<Estudiante> _estudianteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEstudiantesCursosRepository _estudiantesCursosRepository;
        public IRepository<Curso> CursoRepository => _cursoRepository ?? new Repository<Curso>(_dataBaseContext);

        public IRepository<Direccion> DireccionRepository => _direccionRepository ?? new Repository<Direccion>(_dataBaseContext);

        public IRepository<Estudiante> EstudianteRepository => _estudianteRepository ?? new Repository<Estudiante>(_dataBaseContext);
        public IUsuarioRepository UsuarioRepository => _usuarioRepository ?? new UsuarioRepository(_dataBaseContext);
        public IEstudiantesCursosRepository EstudiantesCursosRepository => _estudiantesCursosRepository ?? new EstudiantesCursosRepository(_dataBaseContext);

        public UnitOfWork(PrimeStoneDataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public void ChangeTrackerClear()
        {
            _dataBaseContext.ChangeTracker.Clear();
        }

        public void Dispose()
        {
            if (_dataBaseContext != null)
            {
                _dataBaseContext.Dispose();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dataBaseContext.SaveChangesAsync();
        }
    }
}
