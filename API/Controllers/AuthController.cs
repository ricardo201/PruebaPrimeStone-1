using AutoMapper;
using Common.DTOs;
using Common.Interfaces;
using Common.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly IEncriptService _encriptService;
        public AuthController(IUsuarioService usuarioService, IMapper mapper, IEncriptService encriptService)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _encriptService = encriptService;
        }

        [HttpPost]
        public async Task<IActionResult> Token(UsuarioDto usuarioDto)
        {
            var user = _mapper.Map<Usuario>(usuarioDto);
            user.Password = _encriptService.GetSHA256(user.Password);
            var token = await _usuarioService.Autenticacion(user);
            if (token != null) return Ok(new { token });
            else return Forbid();
        }
    }
}
