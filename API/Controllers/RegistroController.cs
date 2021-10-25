using API.Responses;
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
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly IEncriptService _encriptService;
        public RegistroController(IUsuarioService usuarioService, IMapper mapper, IEncriptService encriptService)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _encriptService = encriptService;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(RegistroDto usuarioDto)
        {
            try
            {
                var usario = _mapper.Map<Usuario>(usuarioDto);
                usario.Password = _encriptService.GetSHA256(usario.Password);
                var registroExitoso = await _usuarioService.Registrar(usario);
                if (!registroExitoso) return StatusCode(500);
                var response = new RespuestaEstandar<RegistroDto>(usuarioDto);
                return Ok(response);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
