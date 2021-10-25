using Common.Const.ErrorMessages;
using Common.Exceptions;
using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EstudianteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Estudiante> SaveOwnerAsync(Estudiante estudiante)
        {
            await _unitOfWork.EstudianteRepository.AddAsync(estudiante);
            await _unitOfWork.SaveChangesAsync();
            return estudiante;
        }

        public async Task<Estudiante> GetEstudianteByIdAsync(int estudianteId)
        {
            return await _unitOfWork.EstudianteRepository.GetByIdAsync(estudianteId);
        }

        public IEnumerable<Estudiante> GetEstudiantes()
        {
            return  _unitOfWork.EstudianteRepository.GetList();
        }

        public async Task<Estudiante> UpdateEstudianteAsync(Estudiante estudiante, int usuarioId)
        {
            var estudianteOld = await _unitOfWork.EstudianteRepository.GetByIdAsync(estudiante.Id);
            if (estudianteOld == null) throw new BusinessException(EstudianteErrorMessages.StudentDoesNotExist);
            var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(usuarioId);
            if(usuario.EstudianteId != estudiante.Id) throw new BusinessException(EstudianteErrorMessages.StudentUpdateNotAllowed);
            estudiante.EstaBorrado = estudianteOld.EstaBorrado;
            estudiante.FechaCreacion = estudianteOld.FechaCreacion;
            _unitOfWork.EstudianteRepository.Update(estudiante);
            await _unitOfWork.SaveChangesAsync();
            return estudiante;
        }

        public async Task<Boolean> DeleteEstudianteAsync(int id, int usuarioId)
        {
            
            var estudianteOld = await _unitOfWork.DireccionRepository.GetByIdAsync(id);
            if (estudianteOld == null) throw new BusinessException(EstudianteErrorMessages.StudentDoesNotExist);
            var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(usuarioId);
            if (estudianteOld.Id != usuario.EstudianteId) throw new BusinessException(EstudianteErrorMessages.StudentUpdateNotAllowed);
            await _unitOfWork.EstudianteRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
            
            return true;
            
        }
    }
}
