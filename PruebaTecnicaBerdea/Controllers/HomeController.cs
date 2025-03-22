using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace PruebaTecnicaBerdea.Controllers
{
    public class HomeController : Controller
    {
        // Caso de uso que se utiliza para buscar Pok�mon.
        private readonly SearchPokemonUseCase _searchPokemonUseCase;

        // HttpClient para realizar solicitudes HTTP a la API de terceros.
        private readonly HttpClient _httpClient;

        // Constructor que recibe una instancia de SearchPokemonUseCase y HttpClient.
        // Esto permite la inyecci�n de dependencias, haciendo que el controlador sea independiente
        // de la implementaci�n concreta del caso de uso y del cliente HTTP.
        public HomeController(SearchPokemonUseCase searchPokemonUseCase, HttpClient httpClient)
        {
            _searchPokemonUseCase = searchPokemonUseCase;
            _httpClient = httpClient;
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

            // Validar el nombre del Pok�mon con la API de terceros.
            bool isValidPokemon = await ValidatePokemonName(pokemonName);
            if (!isValidPokemon)
            {
                ViewBag.Message = "El nombre del Pok�mon no es v�lido.";
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

        // M�todo para validar el nombre del Pok�mon utilizando la API de terceros.
        private async Task<bool> ValidatePokemonName(string pokemonName)
        {
            try
            {
                // URL de la API de Pok�mon para obtener informaci�n de un Pok�mon por nombre.
                var apiUrl = $"https://pokeapi.co/api/v2/pokemon/{pokemonName.ToLower()}";

                // Realizar la solicitud GET a la API.
                var response = await _httpClient.GetAsync(apiUrl);

                // Verificar si la respuesta es exitosa (c�digo 200).
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepci�n (por ejemplo, problemas de red).
                Console.WriteLine($"Error al validar el nombre del Pok�mon: {ex.Message}");
                return false;
            }
        }
    }
}