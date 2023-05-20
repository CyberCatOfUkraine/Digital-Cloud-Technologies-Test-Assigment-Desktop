using ApiScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiScraper.Scrapper
{
    public interface IScraper<T> where T : CryptocurrencyDetails
    {
        /// <summary>
        /// Повертає всі криптовалюти
        /// </summary>
        public List<T> GetAll { get; }

        /// <summary>
        /// Повертає деталі щодо криптовалюти по ідентифікатору
        /// </summary>
        /// <param name="id">Bitcoin,Ethereum,etc.</param>
        /// <returns>Інформація про криптовалюту що містить ціну, обсяг, зміну ціни, на яких ринках її можна придбати і за якою ціною</returns>
        public T GetByCode(string code);


        /// <summary>
        /// Повертає деталі щодо криптовалюти по назві криптовалюти
        /// </summary>
        /// <param name="id">Bitcoin,Ethereum,etc.</param>
        /// <returns>Інформація про криптовалюту що містить ціну, обсяг, зміну ціни, на яких ринках її можна придбати і за якою ціною</returns>
        public T Get(string Name);

        /// <summary>
        /// Здійснює обмін першої криптовалюти на другу
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public decimal Exchange(int value,T first, T second);
    }
}
