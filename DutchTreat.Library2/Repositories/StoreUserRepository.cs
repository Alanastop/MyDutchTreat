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
    public class StoreUserRepository : IEntityRepository<StoreUser>
    {
        public DutchContext DutchContext { get; }
        public ILogger<StoreUserRepository> Logger { get; }

        public StoreUserRepository(DutchContext dutchContext, ILogger<StoreUserRepository> logger)
        {
            this.DutchContext = dutchContext;
            this.Logger = logger;
        }
       
        public void AddEntity(StoreUser entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(int entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StoreUser> GetAll()
        {
            return this.DutchContext.Users; 
        }

        public IEnumerable<StoreUser> GetByUserName(string username)
        {
            return this.DutchContext.Users;
        }

        public StoreUser GetEntityById(int id)
        {
            throw new NotImplementedException();
        }

        public StoreUser GetEntityByName(string username, string email)
        {
            try
            {
                this.Logger.LogInformation("Get user by name was called");
                
                return this.DutchContext.Users.Where(user => (user.Email == email) || (user.UserName == username)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                this.Logger.LogError($"Failed to get current user: {ex}");
                return null;
            }
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(StoreUser entity)
        {
            throw new NotImplementedException();
        }

        public StoreUser GetEntityByIdAndUserName(string username, int id)
        {
            throw new NotImplementedException();
        }
    }
}
