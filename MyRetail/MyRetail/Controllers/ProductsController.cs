using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRetail.Models;
using MyRetail.Services;
using Newtonsoft.Json;

namespace MyRetail.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductPriceService _productPriceService;
        IProductDetailsService _productDetailsService;
        public ProductsController(IProductDetailsService detailService, IProductPriceService priceService)
        {
            _productDetailsService = detailService;
            _productPriceService = priceService;    
        }


        //GET METHOD FOR RESTFUL API//
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {

            //ID validation
            
            if (id<0)
            {
                return NotFound($"{id} : Product Id must be non-negative.Please Enter Valid Product Id");
            }
            var productName = await _productDetailsService.GetProductDetailsAsync(id);

            if (productName.StartsWith("#"))
            {
                return NotFound($" ProductID {id} is not found. Please enter Valid Id");
            }
            Price price;
            try
            {
                price = await _productPriceService.GetProductPrice(id);
            }
            catch (Exception ex)
            {
                return NotFound($"Query Product Price Failed: {ex.Message}, Product Id : {id}");
            }

            return Ok(new Product { Id=id, Name = productName, CurrentPrice = price });
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {   
              
            //PUT validation scenarios
                if (id < 0)
                { 
                return BadRequest($" {id} ProductID cannot be negative in the paramater. Enter Valid product ID");
                  }

                if (id != (product.Id))
                {
                    return BadRequest($"Id in the given parameters and Request body not matching");

                }

                if (product.CurrentPrice.Value < 0)
                {
                    return BadRequest($"Price must be non - negative.Please Enter Valid Price");
                }

        
                var res = await _productPriceService.UpdateProductPrice(id, product.CurrentPrice);

                if (res.StartsWith("#"))
                {
                    return BadRequest($"{res}");
                }

            
            return Ok(res);
        }
    }
}
