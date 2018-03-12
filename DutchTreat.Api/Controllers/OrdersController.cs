using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Library2.Models.Persistent;
using DutchTreat.Library2.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ApiController<Order>
    {
        public IEntityRepository<Order> OrdersRepository { get; }
        public ILogger<OrdersController> Logger { get; }
        public UserManager<StoreUser> UserManager { get; }

        public OrdersController(IEntityRepository<Order> ordersRepository,
            ILogger<OrdersController> logger,
            UserManager<StoreUser> userManager)
        {
            this.OrdersRepository = ordersRepository;
            this.Logger = logger;
            this.UserManager = userManager;
        }

        [HttpGet("/api/orders")]
        public override IActionResult GetAll()
        {
            try
            {
                return Ok(this.OrdersRepository.GetByUserName(User.Identity.Name));
            }
            catch (Exception ex)
            {

                this.Logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("/api/orders/{id}")]
        public override IActionResult GetById(string username, int id)
        {
            try
            {
                var order = this.OrdersRepository.GetEntityByIdAndUserName(User.Identity.Name, id);
                if (order != null)
                    return Ok(order);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {

                this.Logger.LogError($"Failed to get order: {ex}");
                return BadRequest("Failed to get order");
            }
        }

        [HttpPost("/api/orders")]
        public override async Task<IActionResult> Post([FromBody]Order order)
        {
            try
            {
                order.User = await this.UserManager.FindByNameAsync(User.Identity.Name);
                this.OrdersRepository.AddEntity(order);
                if (await this.OrdersRepository.SaveChangesAsync())
                    return Created($"/api/orders/{order.Id}", order);                
            }
            catch (Exception ex)
            {
                this.Logger.LogError($"Failed to save the order: {ex}");                
            }

            return BadRequest("Failed to save the new order");
        }

    }
}