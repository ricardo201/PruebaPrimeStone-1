using Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IDireccionService
    {
        Task<Direccion> SaveDireccionAsync(Direccion direccion);
        Task<Direccion> GetDireccionByIdAsync(int id);
        IEnumerable<Direccion> GetDirecciones(int estudianteId);
        Task<Boolean> DeleteDireccionAsync(int id, int usuarioId);
        Task<Direccion> UpdateDireccionAsync(Direccion direccion, int usuarioId);
    }
}
