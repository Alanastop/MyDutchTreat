using DutchTreat.Library2.DBContext;
using DutchTreat.Library2.Models.Persistent;
using DutchTreat.Library2.Repositories.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Library2.Repositories
{
    public class ProductsRepository : IEntityRepository<Product>
    {
        private readonly DutchContext localDutchContext;

        public ILogger<ProductsRepository> Logger { get; }

        public ProductsRepository(DutchContext dutchContext, ILogger<ProductsRepository> logger)
        {
            this.localDutchContext = dutchContext;
            Logger = logger;
        }        

        public void AddEntity(Product entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(int entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            try
            {
                this.Logger.LogInformation("Get all was called");
                return this.localDutchContext.Products
                    .OrderBy(prod => prod.Title)
                    .ToList();                 
            }
            catch (Exception ex)
            {
                this.Logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetAllProductsByCategory()
        {
            try
            {
                this.Logger.LogInformation("Get all products by category was called");
                return this.localDutchContext.Products
                    .OrderBy(prod => prod.Category)
                    .ToList();
            }
            catch (Exception ex)
            {   
                this.Logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public Product GetEntityByName(string EntityName, string username)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await this.localDutchContext.SaveChangesAsync() > 0);
        }

        public void UpdateEntity(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product GetEntityById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetEntityByIdAndUserName(string username, int id)
        {
            throw new NotImplementedException();
        }
    }
}
