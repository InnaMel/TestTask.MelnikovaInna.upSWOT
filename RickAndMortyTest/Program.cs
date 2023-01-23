using Xunit;
using FluentAssertions;
using FluentValidation;
using System.Linq;
using TestTask.MelnikovaInna.upSWOT.Controllers;
using Microsoft.AspNetCore.Mvc;

class Program
{
    static void Main(string[] args)
    {

    }
    public class RickAndMortyTests22222
    {
        [Theory]
        [InlineData("Rick")]
        public async Task GetCharacterByName_ShouldBeFindAtLeastOne(string nameCharacter)
        {
            var service = new ApiController();

            var result = service.GetCharacter(nameCharacter);
            result.Exception.Should().Be(0);
            result.Result.Value.Should().Be(nameCharacter);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("")]
        public async Task GetCharacterByEmptyName_ShouldBeErrorNotFoud(string nameCharacter)
        {
            var service = new ApiController();

            var result = service.GetCharacter(nameCharacter);
            result.GetType().Should().Be(typeof(NotFoundResult));
        }
    }
}