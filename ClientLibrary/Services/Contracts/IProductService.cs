using BasicLibrary.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Services.Contracts
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(Product model);
        Task<Product> UpdateProductAsync(Product model);
        Task<Product> DeleteProductAsync(int productId);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
    }
}
