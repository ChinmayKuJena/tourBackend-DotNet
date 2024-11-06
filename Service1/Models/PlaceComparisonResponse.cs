using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service1.Models
{
    public class PlaceComparisonResponse
    {
    public string PlaceName { get; set; }
    public Dictionary<string, string> Details { get; set; }
    }
}