using DutchTreat.Library2.DBContext;
using DutchTreat.Library2.Models.Persistent;
using DutchTreat.Library2.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Library2.Repositories
{
    public class OrdersRepository : IEntityRepository<Order>
    {
        public DutchContext DutchContext { get; }
        public ILogger Logger { get; }
       
        public OrdersRepository(DutchContext dutchContext,
            ILogger<OrdersRepository> logger)
        {
            this.DutchContext = dutchContext;
            this.Logger = logger;
        }

        public void AddEntity(Order order)
        {
            try
            {                
                this.Logger.LogInformation("The add entity was called");
                this.DutchContext.Add(order);
            }
            catch (Exception ex)
            {

                this.Logger.LogError($"Failed to get all orders: {ex}");
            }
            
        }

        public void DeleteEntity(int id)
        {
            var localOrder = this.DutchContext.Orders.SingleOrDefault(order => order.Id == id);
            if (localOrder == null)
                throw new ArgumentException();

            this.DutchContext.Orders.Remove(localOrder);
        }

        public IEnumerable<Order> GetAll()
        {
            try
            {
                this.Logger.LogInformation("The get orders was called");
                return this.DutchContext.Orders
                    .Include(order => order.Items)
                    .ThenInclude(orderItem => orderItem.Product)
                    .ToList();
            }
            catch (Exception ex)
            {

                this.Logger.LogError($"Failed to get all orders: {ex}");
                return null;
            }            
        }

        public IEnumerable<Order> GetByUserName(string username)
        {
            try
            {
                this.Logger.LogInformation("The get orders by username was called");
                return this.DutchContext.Orders
                    .Where(order => order.User.UserName == username)
                    .Include(order => order.Items)
                    .ThenInclude(orderItem => orderItem.Product)
                    .ToList();
            }
            catch (Exception ex)
            {

                this.Logger.LogError($"Failed to get all orders: {ex}");
                return null;
            }
        }

        public Order GetEntityByName(string EntityName, string username)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await this.DutchContext.SaveChangesAsync() > 0);
        }

        public void UpdateEntity(Order entity)
        {
            //Convert new products to lookup of product
            entity.Items.ToList().ForEach(item =>
            {
                item.Product = this.DutchContext.Products.Find(item.Product.Id);                
            });

            AddEntity(entity);
        }

        public Order GetEntityById(int id)
        {
            try
            {
                this.Logger.LogInformation("The get orders was called");
                return this.DutchContext.Orders
                    .Include(order => order.Items)
                    .ThenInclude(orderItem => orderItem.Product)
                    .Where(order => order.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {

                this.Logger.LogError($"Failed get all orders: {ex}");
                return null;
            }
        }

        public Order GetEntityByIdAndUserName(string username, int id)
        {
            try
            {
                this.Logger.LogInformation("The get orders was called");
                return this.DutchContext.Orders
                    .Where(order => order.Id == id && order.User.UserName == username)
                    .Include(order => order.Items)
                    .ThenInclude(orderItem => orderItem.Product)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {

                this.Logger.LogError($"Failed get all orders: {ex}");
                return null;
            }
        }
    }
}
