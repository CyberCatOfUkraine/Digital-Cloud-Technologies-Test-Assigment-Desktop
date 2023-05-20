using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DCTTA.Models
{
    /// <summary>
    /// Деталі про криптовалюту
    /// </summary>
    public class Currency
    {
        public Currency(string code, string name, decimal price, decimal volume, decimal priceChange, List<Market> markets, string baseId, int rank, bool isCurrencyVisible)
        {
            Code = code;
            Price = price;
            Volume = volume;
            PriceChange = priceChange;
            Markets = markets;
            BaseId = baseId;
            Name = name;
            Rank = rank;
            IsCurrencyVisible = isCurrencyVisible;
        }

        /// <summary>
        /// Код криптовалюти
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Назва валюти
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ціна
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Обсяг
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// Зміна ціни
        /// </summary>
        public decimal PriceChange { get; set; }
        /// <summary>
        /// Ринки на яких можна придбати
        /// </summary>
        public List<Market> Markets { get; set; }
        /// <summary>
        /// Базова назва криптовалюти
        /// </summary>
        public string BaseId { get; }
        /// <summary>
        /// Ранг
        /// </summary>
        public int Rank { get; set; }
        public bool IsCurrencyVisible { get; set; }
    }
}
