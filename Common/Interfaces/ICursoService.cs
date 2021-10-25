using Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ICursoService
    {
        Task<Curso> SaveCursoAsync(Curso curso);
        Task<Curso> GetCursoByIdAsync(int id);
        IEnumerable<Curso> GetCursos();
        Task<Curso> UpdateCursoAsync(Curso curso);
        Task<Boolean> DeleteCursoAsync(int id);
        Task<IEnumerable<Curso>> GetCursosInscritosAsync(int estudianteId);
        Task<Curso> InscribirCursoAsync(EstudianteCurso estudianteCurso, int usuarioId);
        Task<Boolean> CancelarCursoAsync(EstudianteCurso estudianteCurso, int usuarioId);
    }
}
