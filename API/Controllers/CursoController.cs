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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public CursoController(ICursoService cursoService, IUsuarioService usuarioService, IMapper mapper)
        {
            _cursoService = cursoService;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var cursos = _cursoService.GetCursos();
            var cursosDto = _mapper.Map<IEnumerable<CursoDto>>(cursos);
            var response = new RespuestaEstandar<IEnumerable<CursoDto>>(cursosDto);

            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var curso = await _cursoService.GetCursoByIdAsync(id);
            var cursoDto = _mapper.Map<CursoDto>(curso);
            var response = new RespuestaEstandar<CursoDto>(cursoDto);

            return Ok(response);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        [HttpPost]
        public async Task<IActionResult> Post(CursoDto cursoDto)
        {
            var curso = _mapper.Map<Curso>(cursoDto);           
            curso = await _cursoService.SaveCursoAsync(curso);
            cursoDto = _mapper.Map<CursoDto>(curso);
            var response = new RespuestaEstandar<CursoDto>(cursoDto);

            return Ok(response);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        [HttpPut]
        public async Task<IActionResult> Put(CursoDto cursoDto)
        {
            var curso = _mapper.Map<Curso>(cursoDto);
            curso = await _cursoService.UpdateCursoAsync(curso);
            cursoDto = _mapper.Map<CursoDto>(curso);
            var response = new RespuestaEstandar<CursoDto>(cursoDto);
            
            return Ok(response);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {            
            var result = await _cursoService.DeleteCursoAsync(id);
            var response = new RespuestaEstandar<Boolean>(result);
            return Ok(response);
        }

        [Authorize(Roles = nameof(Rol.Estudiante))]
        [HttpGet()]
        [Route("ListarCursosInscritos")]
        public async Task<IActionResult> ListarCursosInscritos()
        {
            var usuarioId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Contains("IdUser")).Value);
            var usuario = await _usuarioService.GetUsuarioById(usuarioId);
            var curso = await _cursoService.GetCursosInscritosAsync((int)usuario.EstudianteId);
            var cursoDto = _mapper.Map<IEnumerable<CursoDto>>(curso);
            var response = new RespuestaEstandar<IEnumerable<CursoDto>>(cursoDto);

            return Ok(response);
        }

        [Authorize(Roles = nameof(Rol.Estudiante))]
        [HttpPost]
        [Route("InscribirCurso")]
        public async Task<IActionResult> InscribirCurso(InscribirCursoDto inscribirCursoDto)
        {
            var usuarioId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Contains("IdUser")).Value);
            var estudianteCurso = _mapper.Map<EstudianteCurso>(inscribirCursoDto);
            var curso = await _cursoService.InscribirCursoAsync(estudianteCurso, usuarioId);
            var cursoDto = _mapper.Map<CursoDto>(curso);
            var response = new RespuestaEstandar<CursoDto>(cursoDto);

            return Ok(response);
        }

        [Authorize(Roles = nameof(Rol.Estudiante))]
        [HttpDelete]
        [Route("CancelarCurso")]
        public async Task<IActionResult> CancelarCurso(CancelarCursoDto cancelarCursoDto)
        {
            var usuarioId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Contains("IdUser")).Value);            
            var estudianteCurso = _mapper.Map<EstudianteCurso>(cancelarCursoDto);
            Boolean result = await _cursoService.CancelarCursoAsync(estudianteCurso, usuarioId);
            var response = new RespuestaEstandar<Boolean>(result);

            return Ok(response);
        }
    }
}
