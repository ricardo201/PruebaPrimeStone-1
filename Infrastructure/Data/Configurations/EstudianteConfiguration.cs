using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Data.Configurations
{
    class EstudianteConfiguration : IEntityTypeConfiguration<Estudiante>
    {
        public void Configure(EntityTypeBuilder<Estudiante> builder)
        {           
            builder.Property(e => e.Genero)
                .HasConversion(
                    value => value.ToString(),
                    value => (Genero)(Enum.Parse(typeof(Genero), value))
                );
        }
    }
}
