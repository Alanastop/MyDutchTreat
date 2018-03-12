using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Library2.Models.Persistent;
using DutchTreat.Library2.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Api.Controllers
{
    public class StoreUserController : ApiController<StoreUser>
    {
        public IEntityRepository<StoreUser> EntityRepository { get; }
        public ILogger<StoreUserController> Logger { get; }

        public StoreUserController(IEntityRepository<StoreUser> entityRepository, ILogger<StoreUserController> logger)
        {
            this.EntityRepository = entityRepository;
            this.Logger = logger;
        }

        [HttpGet("/api/storeuser")]
        public override IActionResult GetAll()
        {
            try
            {
                var user = new StoreUser();
                return Ok(this.EntityRepository.GetEntityByName(user.UserName, user.Email));
            }
            catch (Exception ex)
            {

                this.Logger.LogError($"Failed to get current user: {ex}");
                return BadRequest("Failed to get current user");
            }
        }

        [HttpPost("/api/storeuser")]
        public override IActionResult GetByUsernameOrEmail([FromBody]StoreUser user)
        {
            try
            {
                return Ok(this.EntityRepository.GetEntityByName(user.UserName, user.Email));
            }
            catch (Exception ex)
            {

                this.Logger.LogError($"Failed to get current user: {ex}");
                return BadRequest("Failed to get current user");
            }
        }

        ////[HttpPost("/api/storeuser")]
        //public override async Task<IActionResult> Post([FromBody]StoreUser user)
        //{
        //    try
        //    {
        //        this.EntityRepository.AddEntity(user);
        //        if (await this.EntityRepository.SaveChangesAsync())
        //            return Created($"/api/orders/{user.Id}", user);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Logger.LogError($"Failed to save the order: {ex}");
        //    }

        //    return BadRequest("Failed to save the new order");
        //}
    }
}