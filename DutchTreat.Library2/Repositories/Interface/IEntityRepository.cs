using DutchTreat.Library2.Models.Persistent.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Library2.Repositories.Interface
{
    public interface IEntityRepository<T> where T:IEntity
    {
        /// <summary>
        /// The get all trips.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<T> GetByUserName(string username);

        IEnumerable<T> GetAll();

        void AddEntity(T entity);

        void UpdateEntity(T entity);

        Task<bool> SaveChangesAsync();

        T GetEntityByName(string EntityName, string username);

        T GetEntityById(int id);

        T GetEntityByIdAndUserName(string username, int id);

        void DeleteEntity(int entity);
    }
}
