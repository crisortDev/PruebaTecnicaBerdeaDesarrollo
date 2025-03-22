using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace PruebaTecnicaBerdea.Controllers
{
    public class HomeController : Controller
    {
        // Caso de uso que se utiliza para buscar Pokémon.
        private readonly SearchPokemonUseCase _searchPokemonUseCase;

        // HttpClient para realizar solicitudes HTTP a la API de terceros.
        private readonly HttpClient _httpClient;

        // Constructor que recibe una instancia de SearchPokemonUseCase y HttpClient.
        // Esto permite la inyección de dependencias, haciendo que el controlador sea independiente
        // de la implementación concreta del caso de uso y del cliente HTTP.
        public HomeController(SearchPokemonUseCase searchPokemonUseCase, HttpClient httpClient)
        {
            _searchPokemonUseCase = searchPokemonUseCase;
            _httpClient = httpClient;
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

            // Validar el nombre del Pokémon con la API de terceros.
            bool isValidPokemon = await ValidatePokemonName(pokemonName);
            if (!isValidPokemon)
            {
                ViewBag.Message = "El nombre del Pokémon no es válido.";
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

        // Método para validar el nombre del Pokémon utilizando la API de terceros.
        private async Task<bool> ValidatePokemonName(string pokemonName)
        {
            try
            {
                // URL de la API de Pokémon para obtener información de un Pokémon por nombre.
                var apiUrl = $"https://pokeapi.co/api/v2/pokemon/{pokemonName.ToLower()}";

                // Realizar la solicitud GET a la API.
                var response = await _httpClient.GetAsync(apiUrl);

                // Verificar si la respuesta es exitosa (código 200).
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción (por ejemplo, problemas de red).
                Console.WriteLine($"Error al validar el nombre del Pokémon: {ex.Message}");
                return false;
            }
        }
    }
}