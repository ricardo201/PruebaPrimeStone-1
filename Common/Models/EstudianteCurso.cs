﻿using System.Collections.Generic;

namespace Common.Models
{
    public class EstudianteCurso : Entidad
    {
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }
        public int CursoId { get; set; }
        public virtual Curso Curso { get; set; }
    }
}