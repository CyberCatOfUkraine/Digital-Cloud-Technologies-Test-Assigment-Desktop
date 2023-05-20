using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiScraper.Models.ForParser
{
    public class CoinCapMarketRoot
    {
        public List<CoinCapMarket> data { get; set; }
        public long timestamp { get; set; }
    }
}
