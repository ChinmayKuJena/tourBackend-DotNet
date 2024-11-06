using System.Collections.Generic;

namespace GroqPlaceInfoApi.Models
{
    public class PlaceInfo
    {
        public string Place { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string> newData { get; set; } = new Dictionary<string, string>();
    }
}
