using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PruebaTecnicaBerdea.Controllers
{
    public class HomeController : Controller
    {
        // Caso de uso que se utiliza para buscar Pokémon.
        private readonly SearchPokemonUseCase _searchPokemonUseCase;

        // Constructor que recibe una instancia de SearchPokemonUseCase.
        // Esto permite la inyección de dependencias, haciendo que el controlador sea independiente
        // de la implementación concreta del caso de uso.
        public HomeController(SearchPokemonUseCase searchPokemonUseCase)
        {
            _searchPokemonUseCase = searchPokemonUseCase;
        }

        // Método que devuelve la vista principal (Index).
        // No recibe parámetros y no realiza operaciones complejas.
        public IActionResult Index()
        {
            return View();
        }

        // Método asíncrono que maneja la búsqueda de un Pokémon.
        // Parámetros:
        // - pokemonName: El nombre del Pokémon que se desea buscar.
        // Retorno:
        // - Una vista con el resultado de la búsqueda.
        [HttpPost] // Indica que este método responde a solicitudes HTTP POST.
        public async Task<IActionResult> SearchPokemon(string pokemonName)
        {
            // Validar que el nombre del Pokémon no esté vacío o sea nulo.
            if (string.IsNullOrEmpty(pokemonName))
            {
                // Si el nombre es inválido, se muestra un mensaje de error en la vista.
                ViewBag.Message = "Por favor, ingresa un nombre válido.";
                return View("Index");
            }

            // Ejecutar el caso de uso para buscar el Pokémon.
            var (message, pokemon) = await _searchPokemonUseCase.Execute(pokemonName);

            // Pasar los resultados a la vista mediante ViewBag.
            ViewBag.Message = message; // Mensaje descriptivo (éxito o error).
            ViewBag.PokemonName = pokemon?.Name; // Nombre del Pokémon encontrado (si existe).

            // Devolver la vista Index con los resultados.
            return View("Index");
        }
    }
}