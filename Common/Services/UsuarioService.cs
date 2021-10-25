using Common.Interfaces;
using Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public UsuarioService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Autenticacion(Usuario usuario)
        {
            var validacion = await EsValido(usuario);
            if (validacion.Item2)
            {
                var token = GenerarToken(validacion.Item1);
                return token;
            }
            return null;
        }

        public async Task<(Usuario, bool)> EsValido(Usuario usuario)
        {
            var usuarioValido = await GetLoginPorCredenciales(usuario);
            return (usuarioValido, usuarioValido != null);
        }

        public string GenerarToken(Usuario usuario)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var singninCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(singninCredentials);
            var claims = new[]
            {
                new Claim("IdUser", usuario.Id.ToString()),
                new Claim("UserName", usuario.UserName),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            };
            var payload = new JwtPayload
            (
                _configuration["Authentication:ValidIssuer"],
                _configuration["Authentication:ValidAudience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(Double.Parse(_configuration["Authentication:LifetimeInMinutes"]))
            );
            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Usuario> GetLoginPorCredenciales(Usuario usuario)
        {
            return await _unitOfWork.UsuarioRepository.GetLoginPorCredenciales(usuario);
        }

        public void GetUsuarioPorToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
        }

        public async Task<bool> Registrar(Usuario usuario)
        {
            try
            {
                await _unitOfWork.UsuarioRepository.AddAsync(usuario);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                ///TODO: Implements logger
                return false;
            }
        }

        public async Task<bool> UserNameNoExiste(string userName)
        {
            return await _unitOfWork.UsuarioRepository.ConteoPorUserNameAsync(userName) == 0;
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _unitOfWork.UsuarioRepository.GetByIdAsync(id);
        }

        public async Task<Usuario> UpdateUsuarioEstudianteAsync(int usuarioId, int estudianteId)
        {
            var usuario = await GetUsuarioById(usuarioId);
            usuario.EstudianteId = estudianteId;
            _unitOfWork.UsuarioRepository.Update(usuario);
            await _unitOfWork.SaveChangesAsync();
            return usuario;
        }
    }
}
