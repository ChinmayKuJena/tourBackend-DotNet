using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service1.Models
{
    public class PlaceComparisonRequest
    {
    public List<string> Places { get; set; }
    public List<string> Criteria { get; set; }
    }
}