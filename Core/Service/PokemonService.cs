using PruebaTecnicaBerdea.Core.Domain;
using System.Threading.Tasks;

public class PokemonService
{
    private readonly IPokemonRepository _pokemonRepository; // Repositorio para acceder a los datos de Pokémon.

    public PokemonService(IPokemonRepository pokemonRepository) // Constructor con inyección de dependencias.
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<Pokemon> GetPokemonByNameAsync(string name) // Obtiene un Pokémon por su nombre.
    {
        return await _pokemonRepository.GetByNameAsync(name); // Llama al repositorio para buscar el Pokémon.
    }

    public async Task AddPokemonAsync(Pokemon pokemon) // Añade un nuevo Pokémon a la base de datos.
    {
        await _pokemonRepository.AddAsync(pokemon); // Llama al repositorio para insertar el Pokémon.
    }
}