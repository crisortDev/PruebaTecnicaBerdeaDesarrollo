using System.Threading.Tasks;

namespace PruebaTecnicaBerdea.Core.Domain
{
    public interface IPokemonRepository
    {
        // Declaración del método (sin cuerpo)
        Task<Pokemon> GetByNameAsync(string name);

        // Declaración del método (sin cuerpo)
        Task AddAsync(Pokemon pokemon);
    }
}