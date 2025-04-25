using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlatRockTechnologyTask.Models
{
    public class ProductInfo
    {

        public ProductInfo(string productName, string price, string rating)
        {
            Name = productName;
            Price = price;
            Rating = rating;
        }
        [JsonPropertyName("productName")]
        public string Name { get; set; }
        [JsonPropertyName("price")]
        public string Price { get; set; }
        [JsonPropertyName("rating")]
        public string Rating { get; set; }
    }
}
