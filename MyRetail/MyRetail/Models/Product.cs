using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MyRetail.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Price CurrentPrice { get; set; }
    }

 
    public class Price 
    {
        [Range(minimum: 1, maximum: long.MaxValue)]
        public double Value { get; set; }
        [Required]

        //JSON required Property Mandatory Field Check

        [JsonProperty(Required = Required.Always)]
        public string? Currency { get; set; }
    }

   

    public class ProductPrice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Id")]
        public int ProductId { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; } = null!;

    }
}
