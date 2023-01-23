using System.Runtime.Serialization;

namespace TestTask.MelnikovaInna.upSWOT.Models
{
    [DataContract]
    public class Person
    {
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
        public PersonOrigin Origin { get; set; }
    }
}
