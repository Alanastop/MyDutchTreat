using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Library2.Models.Persistent;
using DutchTreat.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DutchTreat.MVC.Controllers
{
    public class OrdersMvcController : Controller
    {
        public IMapper Mapper { get; }

        public OrdersMvcController(IMapper mapper)
        {
            Mapper = mapper;
        }        

        public async Task<IActionResult> PostAsync([FromBody]OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var newOrder = new Order();
                newOrder = this.Mapper.Map<OrderViewModel, Order>(orderViewModel);
                

                if (newOrder.OrderDate == DateTime.MinValue)
                    newOrder.OrderDate = DateTime.Now;

               
                var httpClient = new HttpClient();

                var response = await httpClient.PostAsync(
                                   "http://localhost:50939/api/orders/",
                                   new StringContent(JsonConvert.SerializeObject(newOrder), Encoding.UTF8, "application/json"));
                //newOrder = JsonConvert.DeserializeObject<Order>(response.Content.ReadAsStringAsync().Result);
                //var newOrderViewModel = this.Mapper.Map<Order, OrderViewModel>(newOrder);
                return this.Redirect("");
            }
            else
                return BadRequest(ModelState);
        }
    }
}