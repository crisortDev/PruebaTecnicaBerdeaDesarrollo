using Microsoft.EntityFrameworkCore;
using PruebaTecnicaBerdea.Core.Domain;

namespace PruebaTecnicaBerdea.Data
{
    public class ContextDataBase : DbContext
    {
        public DbSet<Pokemon> Pokemons { get; set; }

        public ContextDataBase(DbContextOptions<ContextDataBase> options) : base(options) { }
    }
}
