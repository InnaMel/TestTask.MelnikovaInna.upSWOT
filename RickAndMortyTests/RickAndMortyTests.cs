using Xunit;
using FluentAssertions;
using System.Linq;
using TestTask.MelnikovaInna.upSWOT.Controllers;
using Microsoft.AspNetCore.Mvc;
using TestTask.MelnikovaInna.upSWOT.Models;
using System.Collections;

namespace RickAndMortyTests
{
    public class RickAndMortyTests
    {
        [Theory]
        [InlineData("Rick Sanchez")]
        public async Task GetCharacterByName_ShouldBeFindAtLeastOne(string nameCharacter)
        {
            var service = new ApiController();

            var result = await service.GetCharacter(nameCharacter);
            result.Value.First().Name.Should().Be(nameCharacter);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("NotRick")]
        public async Task GetCharacterByEmptyName_ShouldBeErrorNotFoud(string nameCharacter)
        {
            var service = new ApiController();

            var result = await service.GetCharacter(nameCharacter);
            result.Result.GetType().Name.Should().Be("NotFoundResult");
        }

        [Theory]
        [ClassData(typeof(SearchRequestData))]
        public async Task FindCharacterInEpisode_ShouldBeSuccessfullyFound(SearchRequest request)
        {
            var service = new ApiController();

            var result = await service.FindCharacterInEpisode(request);
            result.Value.Should().Be(true);
        }

        [Theory]
        [ClassData(typeof(SearchRequestErrorData))]
        public async Task FindCharacterInEpisode_ShouldNotBeSuccessfullyFound(SearchRequest request)
        {
            var service = new ApiController();

            var result = await service.FindCharacterInEpisode(request);
            result.Value.Should().Be(false);
            result.Result.GetType().Name.Should().Be("NotFoundResult");
        }

        private class SearchRequestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                new SearchRequest
                {
                  EpisodeName="Meeseeks and Destroy",
                  PersonName="Rick Sanchez"
                }
            };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class SearchRequestErrorData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                new SearchRequest
                {
                  EpisodeName="Meeseeks and Destroy",
                  PersonName="Rickas Sanchez"
                }
            };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}