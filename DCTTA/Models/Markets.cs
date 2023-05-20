namespace DCTTA.Models
{
    /// <summary>
    /// Інформація про ринок
    /// </summary>
    public class Market
    {
        public Market(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        /// <summary>
        /// Назва ринку
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ціна валюти на ринку
        /// </summary>
        public decimal Price { get; set; }
    }
}