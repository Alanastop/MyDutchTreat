using DutchTreat.Library2.Models;
using DutchTreat.Library2.Models.Persistent;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DutchTreat.Library2.DBContext
{
    public class DutchContext : IdentityDbContext<StoreUser>
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseContext"/> class.
        /// </summary>
        public DutchContext(DbContextOptions<DutchContext> options, ILogger<DutchContext> logger)
                : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
    }      
  
}
