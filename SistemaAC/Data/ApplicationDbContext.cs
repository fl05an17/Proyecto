using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaAC.Models;

namespace SistemaAC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<SistemaAC.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<SistemaAC.Models.Actividades> Actividades { get; set; }

        public DbSet<SistemaAC.Models.Horario> Horario { get; set; }

        public DbSet<SistemaAC.Models.Maquinaria> Maquinaria { get; set; }

        public DbSet<SistemaAC.Models.Tarifas> Tarifas { get; set; }

        public DbSet<SistemaAC.Models.Beneficiario> Beneficiario { get; set; }

        public DbSet<SistemaAC.Models.Instructor> Instructor { get; set; }

        public DbSet<SistemaAC.Models.Persona> Persona { get; set; }
    }
}
