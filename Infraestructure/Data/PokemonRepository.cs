using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaBerdea.Core.Domain;
using PruebaTecnicaBerdea.Data;

public class PokemonRepository : IPokemonRepository
{
    private readonly ApplicationDbContext _context; // Contexto de la base de datos.

    public PokemonRepository(ApplicationDbContext context) // Constructor con inyección de dependencias.
    {
        _context = context;
    }

    public async Task<Pokemon> GetByNameAsync(string name) // Obtiene un Pokémon por su nombre.
    {
        return await _context.Pokemons
            .FirstOrDefaultAsync(p => p.Name == name); // Busca el Pokémon en la base de datos.
    }

    public async Task AddAsync(Pokemon pokemon) // Añade un nuevo Pokémon a la base de datos.
    {
        await _context.Pokemons.AddAsync(pokemon); // Añade el Pokémon al DbSet.
        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.
    }
}