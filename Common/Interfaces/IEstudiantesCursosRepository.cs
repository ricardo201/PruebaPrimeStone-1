using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IEstudiantesCursosRepository : IRepository<EstudianteCurso>
    {
        Task<IEnumerable<EstudianteCurso>> GetCursosInscritos(int estudianteId);
        Task<EstudianteCurso> GetEstudianteCurso(EstudianteCurso estudianteCurso);
    }
}
