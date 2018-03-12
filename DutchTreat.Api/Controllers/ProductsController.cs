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
    public class ProductsController : ApiController<Product>
    {
        public IEntityRepository<Product> ProductRepository { get; }
        public ILogger<ProductsController> Logger { get; }

        public ProductsController(IEntityRepository<Product> productRepository, ILogger<ProductsController> logger)
        {
            this.ProductRepository = productRepository;
            Logger = logger;
        }

        [HttpGet("/api/products")]
        public override IActionResult GetAll()
        {
            try
            {               
                return Ok(this.ProductRepository.GetAll());
            }
            catch (Exception ex)
            {

                this.Logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Failed to get products");
            }
        }
    }
}