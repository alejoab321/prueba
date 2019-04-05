using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Personas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Personas
{
    public class PersonasMap : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Personas")
                .HasKey(c => c.IdPersona);
            builder.Property(c => c.Nombre)
                .HasMaxLength(50);
            builder.Property(c => c.Apellidos)
                .HasMaxLength(50);
        }
    }
}
