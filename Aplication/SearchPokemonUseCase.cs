using PruebaTecnicaBerdea.Core.Domain;
using System.Threading.Tasks;

public class SearchPokemonUseCase
{
    private readonly IPokemonRepository _pokemonRepository;

    public SearchPokemonUseCase(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<(string Message, Pokemon Pokemon)> Execute(string pokemonName)
    {
        // Buscar el Pokémon en la base de datos
        var pokemon = await _pokemonRepository.GetByNameAsync(pokemonName);

        if (pokemon == null)
        {
            // Si no existe, crear un nuevo Pokémon
            pokemon = new Pokemon { Name = pokemonName };

            // Insertar el Pokémon en la base de datos
            await _pokemonRepository.AddAsync(pokemon);

            return ("Pokémon no existía y ha sido insertado para futuras consultas.", pokemon);
        }

        return ("Pokémon encontrado en la base de datos.", pokemon);
    }
}