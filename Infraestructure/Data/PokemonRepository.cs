using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaBerdea.Core.Domain;
using PruebaTecnicaBerdea.Data;

public class PokemonRepository : IPokemonRepository
{
    private readonly ApplicationDbContext _context;

    public PokemonRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Pokemon> GetByNameAsync(string name)
    {
        return await _context.Pokemons
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task AddAsync(Pokemon pokemon)
    {
        await _context.Pokemons.AddAsync(pokemon);
        await _context.SaveChangesAsync();
    }
}