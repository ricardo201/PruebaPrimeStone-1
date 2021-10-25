using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    [Table("Estudiante")]
    public class Estudiante : Entidad
    {
        public Estudiante()
        {
            EstudiantesCursos = new HashSet<EstudianteCurso>();
        }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimento { get; set; }
        public Genero Genero { get; set; }
        public IEnumerable<Direccion> Direcciones { get; set; }
        public IEnumerable<EstudianteCurso> EstudiantesCursos { get; set; }
        public Usuario Usuario { get; set; }
    }
}
