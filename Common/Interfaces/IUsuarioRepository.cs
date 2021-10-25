using Common.Models;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<int> ConteoPorUserNameAsync(string userName);
        Task<Usuario> GetLoginPorCredenciales(Usuario usuario);
    }
}
