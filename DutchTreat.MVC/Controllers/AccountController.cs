using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Library2.Models.Persistent;
using DutchTreat.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DutchTreat.MVC.Controllers
{
    public class AccountController : Controller
    {
        public ILogger<AccountController> Logger { get; }
        public SignInManager<StoreUser> Manager { get; }
        public UserManager<StoreUser> RegisterManager { get; }
        public IConfiguration Configuration { get; }

        public AccountController(ILogger<AccountController> logger,
            SignInManager<StoreUser> manager,
            UserManager<StoreUser> registerManager,
            IConfiguration configuration)
        {
            this.Logger = logger;
            this.Manager = manager;
            this.RegisterManager = registerManager;
            this.Configuration = configuration;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "App");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.Manager.PasswordSignInAsync(model.UserName,
                                    model.Password,
                                    model.RememberMe,
                                    false);

                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                        return Redirect(Request.Query["ReturnUrl"].First());
                    else
                        return RedirectToAction("Shop", "App");
                }
            }

            ModelState.AddModelError("", "Failed to login");

            return this.View();
        }

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new StoreUser()
                {
                    FirstName = model.UserName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.UserName
                };


                var result = await this.RegisterManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                        return Redirect(Request.Query["ReturnUrl"].First());
                    else
                        return RedirectToAction("Shop", "App");
                }
                else
                    ModelState.AddModelError("", "Failed to register");

            }

            return this.View();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await this.Manager.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this.RegisterManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await this.Manager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {
                        //Create the token
                        var claims = new[]
                        {
                          new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            this.Configuration["Tokens:Issuer"],
                            this.Configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.Now.AddHours(3),
                            signingCredentials: credentials
                            );

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created("", results);
                    }
                }


            }

            return BadRequest();
        }
    }
}