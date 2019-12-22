using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecept.Models
{
    public class RootObject
    {
        public string Title { get; set; }
        public double Version { get; set; }
        public string Href { get; set; }
        public List<Result> Results { get; set; }
    }
}
