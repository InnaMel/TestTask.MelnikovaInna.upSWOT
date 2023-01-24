using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System;
using TestTask.MelnikovaInna.upSWOT.Models;
using RickAndMortyLib;
using System.Text.Encodings.Web;
using System.Collections.Generic;

namespace TestTask.MelnikovaInna.upSWOT.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ApiController : Controller
    {
        // just for convenience - to show something when debuging
        [HttpGet()]
        public string DefaultPage()
        {
            return "Rick and Morty API test";
        }

        // POST method
        // url should be like http://127.0.0.1:5094/api/v1/person/?name=Rick
        [HttpGet("person")]
        public async Task<ActionResult<Person[]>> GetCharacter(string name)
        {
            Console.WriteLine($"person: {name}");

            if (personCache.TryGetValue(name, out var existingResult))
            {
                Console.WriteLine($"person: returning existing result");
                return existingResult;
            }

            ActionResult<Person[]> result;
            try
            {
                result = await FindPerson(name);
            }
            catch
            {
                result = NotFound();
            }

            personCache.TryAdd(name, result);

            return result;
        }

        [HttpPost("check-person")]
        public async Task<ActionResult<bool>> FindCharacterInEpisode(SearchRequest request)
        {
            Console.WriteLine($"check-person: name {request.PersonName} episode {request.EpisodeName}");

            var key = $"{request.PersonName}_{request.EpisodeName}";

            if (checkPersonCache.TryGetValue(key, out var existingResult))
            {
                Console.WriteLine($"check-person: returning existing result");
                return existingResult;
            }

            ActionResult<bool> result;
            try
            {
                result = await CheckPerson(request);
            }
            catch
            {
                result = NotFound();
            }

            checkPersonCache.TryAdd(key, result);
            return result;
        }

        private async Task<Person[]> FindPerson(string characterName)
        {
            var resultResponseCharacters = new List<Person>();

            var encodedCharacter = UrlEncoder.Default.Encode(characterName);
            var caller = new HttpClientCaller();
            var resultCharacter = await caller.GetAsync($"{RickAndMortyClient.PATHCHARACTER}?name={encodedCharacter}");

            var searchResultByCharacter = RickAndMortyClient.Deserialize<CharecterSearchResult>(resultCharacter);

            foreach (var character in searchResultByCharacter.Results)
            {
                var resultResponseCharacter = new Person();
                var originInfoCharacter = new PersonOrigin();
                caller = new HttpClientCaller();
                var resultOriginInfo = await caller.GetAsync(character.Origin.Url);
                var searchResultOriginInfo = RickAndMortyClient.Deserialize<PersonOrigin>(resultOriginInfo);

                originInfoCharacter.Name = searchResultOriginInfo.Name;
                originInfoCharacter.Type = searchResultOriginInfo.Type;
                originInfoCharacter.Dimension = searchResultOriginInfo.Dimension;

                resultResponseCharacter.Name = character.Name;
                resultResponseCharacter.Status = character.Status;
                resultResponseCharacter.Species = character.Species;
                resultResponseCharacter.Type = character.Type;
                resultResponseCharacter.Gender = character.Gender;
                resultResponseCharacter.Origin = originInfoCharacter;

                resultResponseCharacters.Add(resultResponseCharacter);
            }

            return resultResponseCharacters.ToArray();
        }

        private async Task<bool> CheckPerson(SearchRequest request)
        {
            var containResult = false;

            var encodedEpisode = UrlEncoder.Default.Encode(request.EpisodeName);
            var caller = new HttpClientCaller();
            var resultEpisode = await caller.GetAsync($"{RickAndMortyClient.PATHEPISODE}?name={encodedEpisode}");
            var searchResultByEpisode = RickAndMortyClient.Deserialize<EpisodeSearchResult>(resultEpisode);

            var encodedCharacter = UrlEncoder.Default.Encode(request.PersonName);
            caller = new HttpClientCaller();
            var resultCharacter = await caller.GetAsync($"{RickAndMortyClient.PATHCHARACTER}?name={encodedCharacter}");
            var searchResultByCharacter = RickAndMortyClient.Deserialize<CharecterSearchResult>(resultCharacter);

            foreach (var character in searchResultByCharacter.Results)
            {
                containResult = searchResultByEpisode
                      .Results
                      .FirstOrDefault()
                      .Characters
                      .Contains(character.Url);
                if (containResult) break;
            }

            return containResult;
        }

        private static ConcurrentDictionary<string, ActionResult<Person[]>> personCache = new();
        private static ConcurrentDictionary<string, ActionResult<bool>> checkPersonCache = new();
    }
}