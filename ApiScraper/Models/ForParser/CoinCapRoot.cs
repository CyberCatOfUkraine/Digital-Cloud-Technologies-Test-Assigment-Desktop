using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiScraper.Models.ForParser
{
    internal class CoinCapRoot
    {
        public List<CoinCapDetails> data { get; set; }
        public long timestamp { get; set; }
    }
}
