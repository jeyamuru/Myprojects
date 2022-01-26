using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyRetail.Models;

namespace MyRetail.Services
{
    public interface IProductPriceService
    {
        Task<Price> GetProductPrice(int productId);

        Task<string> UpdateProductPrice(int productId, Price price);
    }
    public class ProductPriceService : IProductPriceService
    {
        private readonly IMongoCollection<ProductPrice> _ProductPriceCollection;

        ProductPriceStoreConfiguration ProductPriceStoreConfiguration { get; set; }
        public ProductPriceService(IOptions<ProductPriceStoreConfiguration> configuration)
        {
            ProductPriceStoreConfiguration= configuration.Value;

            var mongoClient = new MongoClient(
            ProductPriceStoreConfiguration.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ProductPriceStoreConfiguration.DatabaseName);

            IMongoCollection<ProductPrice> mongoCollection = mongoDatabase.GetCollection<ProductPrice>(
                            ProductPriceStoreConfiguration.ProductsCollectionName);
            _ProductPriceCollection = mongoCollection;
        }

        public async Task<Price> GetProductPrice(int productId)
        {
            var productPrice = await _ProductPriceCollection.Find(x => x.ProductId == productId).FirstOrDefaultAsync();

            if(productPrice == null)
            {
                throw new Exception($"Price for ProductId : {productId} not found");
            }

            var price = new Price { Currency = productPrice.Currency, Value = productPrice.Price };

            return price;
        }

        public async Task<string> UpdateProductPrice(int productId, Price price)
        {
            string result = string.Empty;
            try
            {
                var productPrice = await _ProductPriceCollection.Find(x => x.ProductId == productId).FirstOrDefaultAsync();
                if (productPrice == null)

                {
                    //Product ID not found scenario//
                    throw new Exception($"Product Id: {productId} not found");
                }
                var res = await _ProductPriceCollection.ReplaceOneAsync(x => x.ProductId == productId, new ProductPrice { Id = productPrice.Id, ProductId = productId, Currency = price.Currency, Price = price.Value });
                if (res.ModifiedCount == 1)
                    result = "Price Successfully Updated";
                else

                    //Error message Scenario validation when price is not updated//
                    result = $"# No records updated. Please enter valid price to update!";
            }
            catch (Exception ex)
            {
                result = $"#Price Update failed for ProductId : {productId} with error : {ex.Message}!";
            }
            
            return result;
        }
    }
}
