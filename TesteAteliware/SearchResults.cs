using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteAteliware
{
    public class SearchResults
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CloneUrl { get; set; }
        public string SvnUrl { get; set; }
        public int WatchersCount { get; set; }
        public string Homepage { get; set; }
    }
}
