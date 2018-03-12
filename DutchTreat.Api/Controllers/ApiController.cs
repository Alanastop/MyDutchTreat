using DutchTreat.Library2.Models.Persistent.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Api.Controllers
{
    public abstract class ApiController<T> : Controller
        where T : IEntity
    {
        public virtual IActionResult GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual IActionResult GetById(string username, int id)
        {
            throw new NotImplementedException();
        }

        public virtual IActionResult GetByUsernameOrEmail(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IActionResult> Post(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
