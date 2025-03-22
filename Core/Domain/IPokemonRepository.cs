using System.Threading.Tasks;

namespace PruebaTecnicaBerdea.Core.Domain
{
    public interface IPokemonRepository
    {
        // Declaración del método
        Task<Pokemon> GetByNameAsync(string name);

        // Declaración del método
        Task AddAsync(Pokemon pokemon);
    }
}