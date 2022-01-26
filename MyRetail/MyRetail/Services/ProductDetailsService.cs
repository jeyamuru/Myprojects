using Microsoft.Extensions.Options;
using MyRetail.Models;
using Newtonsoft.Json.Linq;

namespace MyRetail.Services
{
    public interface IProductDetailsService
    {
        Task<string> GetProductDetailsAsync(int productId);
    }
    public class ProductDetailsService : IProductDetailsService
    {
        ProductDetailsConfiguration _configuration;
        HttpClient _httpClient;

        public ProductDetailsService(HttpClient http, IOptions<ProductDetailsConfiguration> configuration)
        {
            _configuration = configuration.Value;
            _httpClient = http;
        }

        public async Task<string> GetProductDetailsAsync(int productId)
        {
            //throw new NotImplementedException();
            var productDetailsUrl = $"{_configuration.Url}{productId}";
            
            var res = await _httpClient.GetAsync(productDetailsUrl);

            if (res.IsSuccessStatusCode)
            {
                dynamic result = JObject.Parse(await res.Content.ReadAsStringAsync());

                return result.data.product.item.product_description.title;
            }

            return $"#{res.ReasonPhrase}!";
        }
    }
}
