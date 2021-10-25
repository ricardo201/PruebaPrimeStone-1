using Common.Interfaces;
using Common.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EstudiantesCursosRepository : Repository<EstudianteCurso>, IEstudiantesCursosRepository
    {
        public EstudiantesCursosRepository(PrimeStoneDataBaseContext dataBaseContext) : base(dataBaseContext)
        { }        

        public async Task<IEnumerable<EstudianteCurso>> GetCursosInscritos(int estudianteId)
        {
            return await _entities.Where(item => item.EstudianteId == estudianteId).Include(item => item.Curso).ToListAsync();
        }

        public async Task<EstudianteCurso> GetEstudianteCurso(EstudianteCurso estudianteCurso)
        {
            return await _entities.FirstOrDefaultAsync(item => item.EstudianteId == estudianteCurso.EstudianteId && item.CursoId == estudianteCurso.CursoId);
        }
    }
}