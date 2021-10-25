using Common.Enumerations;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Data.Configurations
{
    class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasOne(d => d.Estudiante)
                   .WithOne(d => d.Usuario)                   
                   .OnDelete(DeleteBehavior.NoAction);
            builder.Property(d => d.EstudianteId)
                   .IsRequired(false);
            builder.Property(e => e.Rol)            
            .HasConversion(
                value => value.ToString(),
                value => (Rol)(Enum.Parse(typeof(Rol), value))
                );
        }
    }
}
