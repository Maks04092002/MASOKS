using Microsoft.EntityFrameworkCore;
using MasoksTech.Core;

namespace MasoksTech.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; } // <--- LÍNEA NUEVA

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Usuarios por defecto en la Base de Datos
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Username = "admin", Password = "123", Rol = "Admin", NombreCompleto = "Administrador MASOKS" },
                new Usuario { Id = 2, Username = "cliente", Password = "123", Rol = "Cliente", NombreCompleto = "Juan Pérez" }
            );
        }
    }
}