using System.Collections.Generic;
using System.Threading.Tasks;
using EjemploPracticaNetCore.BLL;
using EjemploPracticaNetCore.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EjemploPracticaNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IBusiness _businees;

        public ProductController(ILogger<ProductController> logger, IBusiness businees)
        {
            _logger = logger;
            _businees = businees;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
            _logger.LogDebug("In GetAll");
            return await _businees.GetAllProducts();
        }

        [HttpGet ("/{id}")]
        public async Task<Product> Get(int id)
        {
            _logger.LogDebug("In Get");
            return await _businees.GetProductById(id);
        }

        [HttpPost]
        public async Task Post(Product product)
        {
            _logger.LogDebug("In Post");
            await _businees.InsertProduct(product);
            _logger.LogDebug("Out Post");
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            _logger.LogDebug("In Delete");
            await _businees.DeleteProduct(id);
            _logger.LogDebug("Out Delete");
        }

        [HttpPut]
        public async Task Put(Product product)
        {
            _logger.LogDebug("In Put");
            await _businees.UpdateProduct(product);
            _logger.LogDebug("Out Put");
        }
    }
}
