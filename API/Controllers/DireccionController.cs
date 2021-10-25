using API.Responses;
using AutoMapper;
using Common.DTOs;
using Common.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : ControllerBase
    {
        private readonly IDireccionService _direccionService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public DireccionController(IDireccionService direccionService, IUsuarioService usuarioService, IMapper mapper)
        {
            _direccionService = direccionService;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DireccionDto direccionDto)
        {
            var idUser = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Contains("IdUser")).Value);
            var usuario = await _usuarioService.GetUsuarioById(idUser);
            var direccion = _mapper.Map<Direccion>(direccionDto);
            direccion.EstudianteId = (int)usuario.EstudianteId;
            direccion = await _direccionService.SaveDireccionAsync(direccion);
            direccionDto = _mapper.Map<DireccionDto>(direccion);
            var response = new RespuestaEstandar<DireccionDto>(direccionDto);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var direccion = await _direccionService.GetDireccionByIdAsync(id);
            var direccionDto = _mapper.Map<DireccionDto>(direccion);
            var response = new RespuestaEstandar<DireccionDto>(direccionDto);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarioId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Contains("IdUser")).Value);
            var usuario = await _usuarioService.GetUsuarioById(usuarioId);
            var direcciones = _direccionService.GetDirecciones((int)usuario.EstudianteId);
            var direccionesDto = _mapper.Map<IEnumerable<DireccionDto>>(direcciones);
            var response = new RespuestaEstandar<IEnumerable<DireccionDto>>(direccionesDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(DireccionDto direccionDto)
        {
            var direccion = _mapper.Map<Direccion>(direccionDto);
            var usuarioId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Contains("IdUser")).Value);
            var usuario = await _usuarioService.GetUsuarioById(usuarioId);
            direccion = await _direccionService.UpdateDireccionAsync(direccion, (int)usuario.EstudianteId);
            direccionDto = _mapper.Map<DireccionDto>(direccion);
            var response = new RespuestaEstandar<DireccionDto>(direccionDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Contains("IdUser")).Value);
            var result = await _direccionService.DeleteDireccionAsync(id, usuarioId);
            var response = new RespuestaEstandar<Boolean>(result);
            return Ok(response);
        }
    }
}