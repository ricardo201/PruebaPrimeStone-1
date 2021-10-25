using Common.DTOs;
using Common.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Curso, CursoDto>().ReverseMap();
            CreateMap<Direccion, DireccionDto>().ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.StringDireccion));
            CreateMap<DireccionDto, Direccion>().ForMember(dest => dest.StringDireccion, opt => opt.MapFrom(src => src.Direccion));
            CreateMap<Estudiante, EstudianteDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, RegistroDto>().ReverseMap();
            CreateMap<EstudianteCurso, InscribirCursoDto>().ReverseMap();
            CreateMap<EstudianteCurso, CancelarCursoDto>().ReverseMap();
        }
    }
}
