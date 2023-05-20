// See https://aka.ms/new-console-template for more information
using ApiScraper.Scrapper;
using System;

Console.WriteLine("Hello, World!");

/*
CoinCapScraper scraper = new();
scraper.Initialize();

List<ApiScraper.Models.CryptocurrencyDetails> all = scraper.GetAll;

Console.WriteLine(all.Count);

var first = all[0];
var details = scraper.Get(first.Name);
Console.WriteLine(
    details.Name + " : " +
    details.Code + " : " +
    details.Price + " : " +
    details.BaseId + " : " +
    details.Volume + " : " +
    details.PriceChange
    );*/

/*
 * var url = "https://www.google.com/search?q=" + "binanceus" + "+" + "bitcoin" + "+buy";
System.Diagnostics.Process.Start("explorer.exe", $"\"{url}\"");*/
var url = new ApiScraper.Scrapper.CoinCapScraper().GetMarketPage("alterdice", "bitcoin");//.GetMarketPage(CryptoCurrency, Name);
var url1 = new ApiScraper.Scrapper.CoinCapScraper().GetMarketPage("alterdice", "bitcoin");//.GetMarketPage(CryptoCurrency, Name);

System.Diagnostics.Process.Start("explorer.exe", $"\"{url}\"");
System.Diagnostics.Process.Start("explorer.exe", $"\"{url1}\"");

//Console.ReadKey();


