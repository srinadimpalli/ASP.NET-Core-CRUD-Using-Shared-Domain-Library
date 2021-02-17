using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts(bool trackChanges);
        Product GetProduct(int productId, bool trackChanges);
        void CreateProduct(Product product);
        Task CreateProductAsync(Product product);
        void UpdateProduct(Product product);

        void DeleteProduct(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges);
        Task<Product> GetProductAsync(int productId, bool trackChanges);
        int GetCount(bool trackChanges);
    }
}
