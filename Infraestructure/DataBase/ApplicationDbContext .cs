using Microsoft.EntityFrameworkCore;
using PruebaTecnicaBerdea.Core.Domain;

namespace PruebaTecnicaBerdea.Data
{
    public class ApplicationDbContext : DbContext // Hereda de DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet para la tabla Pokemons
        public DbSet<Pokemon> Pokemons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración adicional (opcional)
            modelBuilder.Entity<Pokemon>()
                .HasKey(p => p.Id); // Configura Id como clave primaria
        }
    }
}