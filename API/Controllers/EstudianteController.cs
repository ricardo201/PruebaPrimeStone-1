using API.Responses;
using AutoMapper;
using Common.DTOs;
using Common.Enumerations;
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
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        public EstudianteController(IEstudianteService estudianteService, IUsuarioService usuarioService, IMapper mapper)
        {
            _estudianteService = estudianteService;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [Authorize(Roles = nameof(Rol.Estudiante))]
        [HttpPost]
        public async Task<IActionResult> Post(EstudianteDto estudianteDto)
        {
            var estudiante = _mapper.Map<Estudiante>(estudianteDto);
            estudiante = await _estudianteService.SaveOwnerAsync(estudiante);
            var idUser = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Contains("IdUser")).Value);
            await _usuarioService.UpdateUsuarioEstudianteAsync(idUser, estudiante.Id);
            estudianteDto = _mapper.Map<EstudianteDto>(estudiante);
            var response = new RespuestaEstandar<EstudianteDto>(estudianteDto);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var estudiante = await _estudianteService.GetEstudianteByIdAsync(id);
            var estudianteDto = _mapper.Map<EstudianteDto>(estudiante);
            var response = new RespuestaEstandar<EstudianteDto>(estudianteDto);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var estudiantes = _estudianteService.GetEstudiantes();
            var estudiantesDto = _mapper.Map<IEnumerable<EstudianteDto>>(estudiantes);
            var response = new RespuestaEstandar<IEnumerable<EstudianteDto>>(estudiantesDto);
            return Ok(response);
        }

        [Authorize(Roles = nameof(Rol.Estudiante))]
        [HttpPut]
        public async Task<IActionResult> Put(EstudianteDto estudianteDto)
        {
            var estudiante = _mapper.Map<Estudiante>(estudianteDto);
            var usuarioId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Contains("IdUser")).Value);
            estudiante = await _estudianteService.UpdateEstudianteAsync(estudiante, usuarioId);
            estudianteDto = _mapper.Map<EstudianteDto>(estudiante);
            var response = new RespuestaEstandar<EstudianteDto>(estudianteDto);
            return Ok(response);
        }

        [Authorize(Roles = nameof(Rol.Estudiante))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Contains("IdUser")).Value);
            var result = await _estudianteService.DeleteEstudianteAsync(id, usuarioId);
            var response = new RespuestaEstandar<Boolean>(result);
            return Ok(response);
        }
    }
}
