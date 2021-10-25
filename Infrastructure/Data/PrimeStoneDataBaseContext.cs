using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PrimeStoneDataBaseContext : DbContext
    {
        public virtual DbSet<Curso> Cursos { get; set; }
        public virtual DbSet<Direccion> Direcciones { get; set; }
        public virtual DbSet<Estudiante> Estudiantes { get; set; }
        public virtual DbSet<EstudianteCurso> EstudianteCursos { get; set; }        

        public PrimeStoneDataBaseContext(DbContextOptions<PrimeStoneDataBaseContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Entidad>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
