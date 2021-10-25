using Common.Models;
using System;

namespace Common.DTOs
{
    public class EstudianteDto : Dto
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimento { get; set; }
        public Genero Genero { get; set; }
    }
}
