using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;
using FlatRockTechnologyTask.Models;
using HtmlAgilityPack;

namespace FlatRockTechnologyTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var htmlPath = Path.Combine(projectRoot, "index.html");
            var html = File.ReadAllText(htmlPath);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var elements = doc.DocumentNode.SelectNodes("//div[@class='item']");

            var products = new List<ProductInfo>();

            foreach (var element in elements)
            {
                var imgNode = element.SelectSingleNode(".//img");
                var productName = HttpUtility.HtmlDecode(imgNode?.GetAttributeValue("alt", ""));

                var priceNode = element.SelectSingleNode(".//span[@class='price-display formatted']");
                var priceText = priceNode?.SelectSingleNode(".//span[@style='display: none']")?.InnerText;

                var price = priceText?.Substring(1).Replace(",", "");

                var ratingAttr = element.GetAttributeValue("rating", "0");
                var rating = double.Parse(ratingAttr);

                if (rating > 5)
                {
                    rating /= 2;
                }

                var ratingStr = rating.ToString();

                var productInfo = new ProductInfo(productName, price, ratingStr);


                products.Add(productInfo);
            }

            var jsonOutput = JsonSerializer.Serialize(products, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            Console.WriteLine(jsonOutput);
        }
    }
}
