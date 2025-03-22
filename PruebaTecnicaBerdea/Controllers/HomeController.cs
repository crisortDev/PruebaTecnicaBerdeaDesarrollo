using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PruebaTecnicaBerdea.Controllers
{
    public class HomeController : Controller
    {
        // Caso de uso que se utiliza para buscar Pok�mon.
        private readonly SearchPokemonUseCase _searchPokemonUseCase;

        // Constructor que recibe una instancia de SearchPokemonUseCase.
        // Esto permite la inyecci�n de dependencias, haciendo que el controlador sea independiente
        // de la implementaci�n concreta del caso de uso.
        public HomeController(SearchPokemonUseCase searchPokemonUseCase)
        {
            _searchPokemonUseCase = searchPokemonUseCase;
        }

        // M�todo que devuelve la vista principal (Index).
        // No recibe par�metros y no realiza operaciones complejas.
        public IActionResult Index()
        {
            return View();
        }

        // M�todo as�ncrono que maneja la b�squeda de un Pok�mon.
        // Par�metros:
        // - pokemonName: El nombre del Pok�mon que se desea buscar.
        // Retorno:
        // - Una vista con el resultado de la b�squeda.
        [HttpPost] // Indica que este m�todo responde a solicitudes HTTP POST.
        public async Task<IActionResult> SearchPokemon(string pokemonName)
        {
            // Validar que el nombre del Pok�mon no est� vac�o o sea nulo.
            if (string.IsNullOrEmpty(pokemonName))
            {
                // Si el nombre es inv�lido, se muestra un mensaje de error en la vista.
                ViewBag.Message = "Por favor, ingresa un nombre v�lido.";
                return View("Index");
            }

            // Ejecutar el caso de uso para buscar el Pok�mon.
            var (message, pokemon) = await _searchPokemonUseCase.Execute(pokemonName);

            // Pasar los resultados a la vista mediante ViewBag.
            ViewBag.Message = message; // Mensaje descriptivo (�xito o error).
            ViewBag.PokemonName = pokemon?.Name; // Nombre del Pok�mon encontrado (si existe).

            // Devolver la vista Index con los resultados.
            return View("Index");
        }
    }
}