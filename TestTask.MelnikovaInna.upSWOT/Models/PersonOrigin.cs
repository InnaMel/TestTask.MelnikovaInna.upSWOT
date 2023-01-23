using System.Runtime.Serialization;

namespace TestTask.MelnikovaInna.upSWOT.Models
{
    [DataContract]
    public class PersonOrigin
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "dimension")]
        public string Dimension { get; set; }
    }
}