using Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Usuario : Entidad
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Rol Rol { get; set; }
        public int? EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }
    }
}
