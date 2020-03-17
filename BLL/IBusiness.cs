using EjemploPracticaNetCore.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EjemploPracticaNetCore.BLL
{
    public interface IBusiness
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task DeleteProduct(int id);
        Task InsertProduct(Product product);
        Task UpdateProduct(Product product);
    }
}
