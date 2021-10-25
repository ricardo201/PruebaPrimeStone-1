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
    public class DireccionService : IDireccionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DireccionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Direccion> SaveDireccionAsync(Direccion direccion)
        {
            await _unitOfWork.DireccionRepository.AddAsync(direccion);
            await _unitOfWork.SaveChangesAsync();
            return direccion;
        }
        public async Task<Direccion> GetDireccionByIdAsync(int id)
        {
            return await _unitOfWork.DireccionRepository.GetByIdAsync(id);
        }

        public IEnumerable<Direccion> GetDirecciones(int estudianteID)
        {
            return (IEnumerable<Direccion>)_unitOfWork.DireccionRepository.GetList().Where(direccion => direccion.EstudianteId == estudianteID).ToList();
        }

        public async Task<Direccion> UpdateDireccionAsync(Direccion direccion, int estudianteId)
        {
            var direccionOld = await _unitOfWork.DireccionRepository.GetByIdAsync(direccion.Id);
            if (direccionOld == null) throw new BusinessException(DireccionErrorMessages.AddressDoesNotExist);            
            if (direccionOld.EstudianteId != estudianteId) throw new BusinessException(DireccionErrorMessages.AddressUpdateNotAllowed);
            direccion.EstaBorrado = direccionOld.EstaBorrado;
            direccion.FechaCreacion = direccionOld.FechaCreacion;
            direccion.EstudianteId = direccionOld.EstudianteId;
            _unitOfWork.DireccionRepository.Update(direccion);
            await _unitOfWork.SaveChangesAsync();
            return direccion;
        }

        public async Task<Boolean> DeleteDireccionAsync(int id, int usuarioId)
        {
            var direccionOld = await _unitOfWork.DireccionRepository.GetByIdAsync(id);
            if (direccionOld == null) throw new BusinessException(DireccionErrorMessages.AddressDoesNotExist);
            var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(usuarioId);
            if (direccionOld.EstudianteId != usuario.EstudianteId) throw new BusinessException(DireccionErrorMessages.AddressDeleteNotAllowed);
            await _unitOfWork.DireccionRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
            
            return true;
        }
    }
}
