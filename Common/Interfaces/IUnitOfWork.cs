using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IUnitOfWork
    {   
        IRepository<Curso> CursoRepository { get; }
        IRepository<Direccion> DireccionRepository { get; }
        IRepository<Estudiante> EstudianteRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }
        IEstudiantesCursosRepository EstudiantesCursosRepository { get; }        
        void Dispose();
        Task SaveChangesAsync();
        void ChangeTrackerClear();
    }
}
