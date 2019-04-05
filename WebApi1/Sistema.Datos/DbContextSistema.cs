using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Personas;
using Sistema.Entidades.Personas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<Persona> Personas { get; set; }
        public DbContextSistema(DbContextOptions<DbContextSistema> options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PersonasMap());

        }
    }
}
