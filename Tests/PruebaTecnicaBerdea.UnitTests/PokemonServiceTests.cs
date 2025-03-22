using Moq;
using System.Threading.Tasks;
using Xunit;
using PruebaTecnicaBerdea.Core.Domain;

public class PokemonServiceTests
{
    [Fact]
    public async Task GetPokemonByNameAsync_ReturnsPokemon_WhenPokemonExists()
    {
        // Arrange
        var mockRepository = new Mock<IPokemonRepository>();
        var expectedPokemon = new Pokemon { Name = "pikachu" };

        mockRepository
            .Setup(repo => repo.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedPokemon);

        var pokemonService = new PokemonService(mockRepository.Object);

        // Act
        var result = await pokemonService.GetPokemonByNameAsync("pikachu");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedPokemon.Name, result.Name);
    }

    [Fact]
    public async Task AddPokemonAsync_AddsPokemon_WhenPokemonIsValid()
    {
        // Arrange
        var mockRepository = new Mock<IPokemonRepository>();
        var pokemon = new Pokemon { Name = "charmander" };

        mockRepository
            .Setup(repo => repo.AddAsync(It.IsAny<Pokemon>()))
            .Returns(Task.CompletedTask);

        var pokemonService = new PokemonService(mockRepository.Object);

        // Act
        await pokemonService.AddPokemonAsync(pokemon);

        // Assert
        mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Pokemon>()), Times.Once);
    }
}