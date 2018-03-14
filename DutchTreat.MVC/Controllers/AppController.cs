using DutchTreat.Library2.DBContext;
using DutchTreat.Library2.Models.Persistent;
using DutchTreat.MVC.Services.Interfaces;
using DutchTreat.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DutchTreat.MVC.Controllers
{
    public class AppController: Controller
    {
        private readonly IMailService localMailService;

        public AppController(IMailService mailService)
        {
            this.localMailService = mailService;
        }            

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            return this.View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //this.localMailService.SendMail(model.Email, "alanastop@outlook.com", model.Subject, model.Message);
                this.localMailService.SendMessage("alanastop@outlook.com", model.Subject, $"From: {model.Name} - {model.Email}, Message:{model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }                
          
           return this.View(); 
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";

            return this.View();
        }

        public IActionResult Shop()
        {
            List<Product> products = null;
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("http://localhost:50939/api/products").Result;
            if (response.IsSuccessStatusCode)
            {
                var stateInfo = response.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(stateInfo);
                return this.View(products);
            }
            else
                return BadRequest("Couldn't connect to database");
        } 
    }
}
