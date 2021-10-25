using Common.Interfaces;
using Common.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(PrimeStoneDataBaseContext dataBaseContext) : base(dataBaseContext)
        { }
        public async Task<int> ConteoPorUserNameAsync(string userName)
        {
            return (await _entities.CountAsync(item => item.UserName == userName && item.EstaBorrado == false));
        }

        public async  Task<Usuario> GetLoginPorCredenciales(Usuario usuario)
        {
            return await _entities.FirstOrDefaultAsync(item => item.UserName == usuario.UserName && item.Password == usuario.Password && item.EstaBorrado == false);
        }
    }
}
