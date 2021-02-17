using Microsoft.EntityFrameworkCore;
using SharedDomain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Services
{
    public class ProductService : ServiceBase<Product>, IProductService
    {
        public ProductService(CustOrdProdContext custOrdProdContext) : base(custOrdProdContext)
        {

        }

        public void CreateProduct(Product product)
        {
            Create(product);
        }

        public async Task CreateProductAsync(Product product)
        {
            await CreateAsync(product);
        }

        public void DeleteProduct(Product product)
        {
            Delete(product);
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return FindAll(trackChanges)
                 .OrderBy(c => c.Name)
                 .ToList();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
          .OrderBy(c => c.Name)
          .ToListAsync();
        }

        public Product GetProduct(int productId, bool trackChanges)
        {
            return FindByCondition(c => c.Id.Equals(productId), trackChanges)
                   .SingleOrDefault();
        }

        public async Task<Product> GetProductAsync(int productId, bool trackChanges)
        {
            return await FindByCondition(c => c.Id.Equals(productId), trackChanges)
                        .SingleOrDefaultAsync();
        }

        public void UpdateProduct(Product product)
        {
            Update(product);
        }
        public int GetCount(bool trackChanges)
        {
            return FindAll(trackChanges).Count();
        }
    }
}
