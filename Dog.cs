using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_dogClient_gyakorlo
{
    public enum Gender
    {
        Female,Male
    }
    public class Dog
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Size { get; set; }
        public float Age { get; set; }
        public bool Neutered { get; set; }
        public Gender Gender { get; set;}

        public bool Vaccinated { get; set; }

        public string Diagnosis { get; set; }
        public string Medication { get; set; }
        [JsonProperty("Visit")]
        public DateTime VisitedOn { get; set; }

    }
}
