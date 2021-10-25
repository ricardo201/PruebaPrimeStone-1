using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class EstudianteCursosConfiguration : IEntityTypeConfiguration<EstudianteCurso>
    {
        public void Configure(EntityTypeBuilder<EstudianteCurso> builder)
        {
            builder.HasOne(d => d.Curso)
                   .WithMany(d => d.EstudiantesCursos)
                   .HasForeignKey(d => d.CursoId)
                   .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(d => d.Estudiante)
                   .WithMany(d => d.EstudiantesCursos)
                   .HasForeignKey(d => d.EstudianteId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
