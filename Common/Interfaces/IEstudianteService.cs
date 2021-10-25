using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IEstudianteService
    {
        Task<Estudiante> SaveOwnerAsync(Estudiante estudiante);
        Task<Estudiante> GetEstudianteByIdAsync(int estudianteId);
        IEnumerable<Estudiante> GetEstudiantes();
        Task<Estudiante> UpdateEstudianteAsync(Estudiante estudiante, int usuarioId);
        Task<Boolean> DeleteEstudianteAsync(int id, int usuarioId);
    }
}
