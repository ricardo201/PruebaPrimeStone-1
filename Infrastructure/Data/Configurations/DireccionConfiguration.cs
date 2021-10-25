using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Data.Configurations
{
    class DireccionConfiguration : IEntityTypeConfiguration<Direccion>
    {
        public void Configure(EntityTypeBuilder<Direccion> builder)
        {            
            builder.HasOne(d => d.Estudiante)
                   .WithMany(d => d.Direcciones)
                   .HasForeignKey(d => d.EstudianteId)
                   .OnDelete(DeleteBehavior.NoAction);
            builder.Property(e => e.TipoDireccion)
                .HasConversion(
                    value => value.ToString(),
                    value => (TipoDireccion)(Enum.Parse(typeof(TipoDireccion), value))
                );
        }
    }
}
