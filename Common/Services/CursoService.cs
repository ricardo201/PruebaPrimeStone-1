using Common.Const.ErrorMessages;
using Common.Exceptions;
using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class CursoService : ICursoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CursoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Curso> SaveCursoAsync(Curso curso)
        {
            await _unitOfWork.CursoRepository.AddAsync(curso);
            await _unitOfWork.SaveChangesAsync();
            return curso;
        }
        public async Task<Curso> GetCursoByIdAsync(int id)
        {
            return await _unitOfWork.CursoRepository.GetByIdAsync(id);
        }

        public IEnumerable<Curso> GetCursos()
        {
            return (IEnumerable<Curso>)_unitOfWork.CursoRepository.GetList().ToList();
        }

        public async Task<Curso> UpdateCursoAsync(Curso curso)
        {
            var cursoOld = await _unitOfWork.CursoRepository.GetByIdAsync(curso.Id);
            if (cursoOld == null) throw new BusinessException(CursoErrorMessages.CourseDoesNotExist);            
            curso.EstaBorrado = cursoOld.EstaBorrado;
            curso.FechaCreacion = cursoOld.FechaCreacion;            
            _unitOfWork.CursoRepository.Update(curso);
            await _unitOfWork.SaveChangesAsync();

            return curso;
        }

        public async Task<Boolean> DeleteCursoAsync(int id)
        {
            var cursoOld = await _unitOfWork.CursoRepository.GetByIdAsync(id);
            if (cursoOld == null) throw new BusinessException(CursoErrorMessages.CourseDoesNotExist);
            await _unitOfWork.CursoRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
            
            return true;            
        }

        public async Task<IEnumerable<Curso>> GetCursosInscritosAsync(int estudianteId)
        {            
            var estudianteCursos =await _unitOfWork.EstudiantesCursosRepository.GetCursosInscritos(estudianteId);
            return estudianteCursos.Select(item => item.Curso);            
        }

        public async Task<Curso> InscribirCursoAsync(EstudianteCurso estudianteCurso, int usuarioId)
        {
            var curso = await _unitOfWork.CursoRepository.GetByIdAsync(estudianteCurso.CursoId);
            if(curso == null) throw new BusinessException(CursoErrorMessages.CourseDoesNotExist);
            var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(usuarioId);
            estudianteCurso.EstudianteId = (int)usuario.EstudianteId;
            await _unitOfWork.EstudiantesCursosRepository.AddAsync(estudianteCurso);
            await _unitOfWork.SaveChangesAsync();
            
            return curso;
        }

        public async Task<Boolean> CancelarCursoAsync(EstudianteCurso estudianteCurso, int usuarioId)
        {
              
            var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(usuarioId);
            estudianteCurso.EstudianteId = (int) usuario.EstudianteId;
            var estudianteCursoOld = await _unitOfWork.EstudiantesCursosRepository.GetEstudianteCurso(estudianteCurso);                
            if(estudianteCursoOld == null) throw new BusinessException(CursoErrorMessages.CourseDoesNotExist);                
            await _unitOfWork.EstudiantesCursosRepository.DeleteByIdAsync(estudianteCursoOld.Id);
            await _unitOfWork.SaveChangesAsync();

            return true;
            
        }
    }
}
