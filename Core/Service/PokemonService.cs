using PruebaTecnicaBerdea.Core.Domain;
using System.Threading.Tasks;

public class PokemonService
{
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<Pokemon> GetPokemonByNameAsync(string name)
    {
        return await _pokemonRepository.GetByNameAsync(name);
    }

    public async Task AddPokemonAsync(Pokemon pokemon)
    {
        await _pokemonRepository.AddAsync(pokemon);
    }
}