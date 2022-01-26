using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

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
  [JsonProperty ("Value", Required = Required.Default)]
        public double Value { get; set; }
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
