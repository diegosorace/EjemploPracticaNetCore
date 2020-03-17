using System.Collections.Generic;
using System.Threading.Tasks;
using EjemploPracticaNetCore.DB;
using EjemploPracticaNetCore.Model;
using Microsoft.Extensions.Logging;

namespace EjemploPracticaNetCore.BLL
{
    public class Business : IBusiness
    {
        private readonly ILogger<Business> _logger;
        private readonly DataBaseContext _db;

        public Business(ILogger<Business> logger, DataBaseContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task InsertProduct(Product product)
        {
            try
            {
                _logger.LogDebug("In InsertProduct");

                await Task.Run(() =>
                {
                    _db.Products.Add(product);
                    _db.SaveChanges();
                });
                
                _logger.LogDebug("Out InsertProduct");
            }
            catch (System.Exception)
            {
                _logger.LogError("Error in method InsertProduct");
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                _logger.LogDebug("In DeleteProduct");

                await Task.Run(() => {
                    _db.Products.Remove(_db.Products.Find(id));
                    _db.SaveChanges();
                });

                _logger.LogDebug("Out DeleteProduct");
            }
            catch (System.Exception)
            {
                _logger.LogError("Error in method DeleteProduct");
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                _logger.LogDebug("In GetAllProducts");

                return await Task.Run(() => {
                     return _db.Products;
                });
            }
            catch (System.Exception)
            {
                _logger.LogError("Error in method GetAllProducts");
                return default;
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                _logger.LogDebug("In GetProductById");

                return await Task.Run(() => {
                    return _db.Products.Find(id);
                });
            }
            catch (System.Exception)
            {
                _logger.LogError("Error in method GetProductById");
                return default;
            }   
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                _logger.LogDebug("In UpdateProduct");
                await Task.Run(() => {
                    _db.Products.Add(product);
                    _db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                });
                _logger.LogDebug("Out UpdateProduct");
            }
            catch (System.Exception)
            {
                _logger.LogError("Error in method UpdateProduct");
            }   
        }
    }
}
