using DutchTreat.Library2.DBContext;
using DutchTreat.Library2.Models.Persistent;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Library2.Models.Data
{
    public class DutchSeeder
    {
        public DutchContext DutchContext { get; }
        public IHostingEnvironment Hosting { get; }
        public UserManager<StoreUser> Manager { get; }

        public DutchSeeder(DutchContext dutchContext, IHostingEnvironment hosting, UserManager<StoreUser> manager)
        {
           this.DutchContext = dutchContext;
            this.Hosting = hosting;
            Manager = manager;
        }

        public async Task Seed()
        {
            this.DutchContext.Database.EnsureCreated();
            var user = await this.Manager.FindByEmailAsync("alanastop@outlook.com");

            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Alex",
                    LastName = "Anastopoulos",
                    Email = "alanastop@outlook.com",
                    UserName = "Dark_Ark"
                };

                var result = await this.Manager.CreateAsync(user, "Passw0rd!");

                if (result != IdentityResult.Success)
                    throw new InvalidOperationException("Failed to create user");
            }

            if (!this.DutchContext.Products.Any())
            {
                //Creating sample data
                var filepath = Path.Combine("Models/Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

                this.DutchContext.AddRange(products);

                var order = new List<Order>()
                {
                    new Order()
                    {
                        OrderDate = DateTime.Now,
                        OrderNumber = "123456",
                        Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                             Product = products.First(),
                             Quantity = 5,
                             UnitPrice = products.First().Price
                        },

                        new OrderItem()
                        {
                            Product = products.Skip(1).First(),
                             Quantity = 10,
                             UnitPrice = products.Skip(1).First().Price
                        },

                        new OrderItem()
                        {
                            Product = products.Skip(3).First(),
                             Quantity = 5,
                             UnitPrice = products.Skip(3).First().Price
                        },

                        new OrderItem()
                        {
                            Product = products.Skip(4).First(),
                             Quantity = 8,
                             UnitPrice = products.Skip(4).First().Price
                        },

                        new OrderItem()
                        {
                            Product = products.Skip(5).First(),
                             Quantity = 15,
                             UnitPrice = products.Skip(5).First().Price
                        }
                    },
                        User = user

                    },

                      new Order()
                      {
                        OrderDate = DateTime.Now,
                        OrderNumber = "456786",
                        Items = new List<OrderItem>()
                        {
                            new OrderItem()
                            {
                                 Product = products.Last(),
                                 Quantity = 5,
                                 UnitPrice = products.Last().Price
                            },

                            new OrderItem()
                            {
                                Product = products.Skip(3).First(),
                                 Quantity = 10,
                                 UnitPrice = products.Skip(3).First().Price
                            },

                            new OrderItem()
                            {
                                Product = products.Skip(2).First(),
                                 Quantity = 5,
                                 UnitPrice = products.Skip(2).First().Price
                            },

                            new OrderItem()
                            {
                                Product = products.Skip(4).First(),
                                 Quantity = 8,
                                 UnitPrice = products.Skip(4).First().Price
                            },

                            new OrderItem()
                            {
                                Product = products.Skip(1).First(),
                                 Quantity = 15,
                                 UnitPrice = products.Skip(1).First().Price
                            }
                        },
                        User = user
                      },

                        new Order()
                    {
                        OrderDate = DateTime.Now,
                    OrderNumber = "89765",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                             Product = products.First(),
                             Quantity = 5,
                             UnitPrice = products.First().Price
                        },

                        new OrderItem()
                        {
                            Product = products.Skip(2).First(),
                             Quantity = 10,
                             UnitPrice = products.Skip(2).First().Price
                        },

                        new OrderItem()
                        {
                            Product = products.Skip(1).First(),
                             Quantity = 5,
                             UnitPrice = products.Skip(1).First().Price
                        },

                        new OrderItem()
                        {
                            Product = products.Skip(6).First(),
                             Quantity = 8,
                             UnitPrice = products.Skip(6).First().Price
                        },

                        new OrderItem()
                        {
                            Product = products.Skip(5).First(),
                             Quantity = 15,
                             UnitPrice = products.Skip(5).First().Price
                        }
                    },
                        User = user
                    }
                };

                this.DutchContext.Orders.AddRange(order);
                this.DutchContext.SaveChanges();
            }

        }
    }
}
