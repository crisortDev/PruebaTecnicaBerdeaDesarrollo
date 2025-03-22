using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PruebaTecnicaBerdea.Controllers
{
    public class HomeController : Controller
    {
        private readonly SearchPokemonUseCase _searchPokemonUseCase;

        public HomeController(SearchPokemonUseCase searchPokemonUseCase)
        {
            _searchPokemonUseCase = searchPokemonUseCase;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchPokemon(string pokemonName)
        {
            if (string.IsNullOrEmpty(pokemonName))
            {
                ViewBag.Message = "Por favor, ingresa un nombre válido.";
                return View("Index");
            }

            // Ejecutar el caso de uso
            var (message, pokemon) = await _searchPokemonUseCase.Execute(pokemonName);

            // Pasar los resultados a la vista
            ViewBag.Message = message;
            ViewBag.PokemonName = pokemon?.Name;

            return View("Index");
        }
    }
}