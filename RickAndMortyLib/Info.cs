using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMortyLib
{
    [DataContract]
    public class Info
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "pages")]
        public int Pages { get; set; }

        [DataMember(Name = "next")]
        public string? Next { get; set; }

        [DataMember(Name = "prev")]
        public string? Prev { get; set; }
    }

    [DataContract]
    public class Character
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "species")]
        public string Species { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        [DataMember(Name = "origin")]
        public CharacterOrigin Origin { get; set; }

        [DataMember(Name = "location")]
        public CharacterLocation Location { get; set; }

        [DataMember(Name = "image")]
        public string Image { get; set; }

        [DataMember(Name = "episodes")]
        public IEnumerable<string> Episodes { get; set; }

        [DataMember(Name = "created")]
        public string Created { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }

    [DataContract]
    public class CharacterOrigin
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }

    [DataContract]
    public class CharacterLocation
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }

    [DataContract]
    public class CharecterSearchResult
    {
        [DataMember(Name = "info")]
        public Info Info { get; set; }
        
        [DataMember(Name = "results")]
        public IEnumerable<Character> Results;
    }

    [DataContract]
    public class Episode
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "air_date")]
        public string Air_date { get; set; }
        
        [DataMember(Name = "episodeName")]
        public string EpisodeName { get; set; }

        [DataMember(Name = "characters")]
        public IEnumerable<string> Characters { get; set; }
        
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "created")]
        public string Created { get; set; }
    }

    [DataContract]
    public class EpisodeSearchResult
    {
        [DataMember(Name = "info")]
        public Info Info { get; set; }
        
        [DataMember(Name = "results")]
        public IEnumerable<Episode> Results;
    }
}
