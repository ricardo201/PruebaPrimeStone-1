using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IUsuarioService
    {
        string GenerarToken(Usuario usuario);
        void GetUsuarioPorToken(string token);
        Task<bool> Registrar(Usuario usuario);
        Task<Usuario> GetLoginPorCredenciales(Usuario usuario);
        Task<(Usuario, bool)> EsValido(Usuario usuario);
        Task<string> Autenticacion(Usuario usuario);
        Task<bool> UserNameNoExiste(string userName);
        Task<Usuario> GetUsuarioById(int id);
        Task<Usuario> UpdateUsuarioEstudianteAsync(int usuarioId, int estudianteId);
    }
}
