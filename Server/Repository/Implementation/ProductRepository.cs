using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Server.Model;
using Server.Repository.Base;
using Server.Repository.Interface;
using Server.Utils;

namespace Server.Repository.Implementation
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return (await FindAll()).OrderBy(product => product.Name);
        }

        public async Task<Product> GetProductById(long productID)
        {
            return (await FindByCondition(p => p.Id.Equals(productID)))
                .DefaultIfEmpty(null)
                .FirstOrDefault();
        }

        public async Task<bool> CreateProduct(Product product)
        {
            await Create(product);
            return await Save();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            return await Update(product);
        }

        public async Task<bool> RemoveProduct(long productId)
        {
            var product = await GetProductById(productId);

            if (product != null)
            {
                return await Remove(product);
            }

            return false;
        }
    }
}