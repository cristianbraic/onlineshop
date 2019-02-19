using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineStore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using OnlineStore.ViewModels;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using OnlineStore.Data;

namespace OnlineStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<StoreUser> _signInManager;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly StoreContext _ctx;
        private readonly IStoreRepository _repository;

        public AccountController(
            ILogger<AccountController> logger,
            SignInManager<StoreUser> singInManager,
            UserManager<StoreUser> userManager,
            IConfiguration config,
            IMapper mapper,
            StoreContext ctx,
            IStoreRepository repository)
        {
            _logger = logger;
            _signInManager = singInManager;
            _userManager = userManager;
            _config = config;
            _mapper = mapper;
            _ctx = ctx;
            _repository = repository;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var result = await _signInManager.PasswordSignInAsync(
                            model.LoginModel.Username,
                            model.LoginModel.Password,
                            model.LoginModel.RememberMe,
                            false);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        return RedirectToAction("Shop", "App");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Autentificare nereusita");
                }
                
            }

            return View();
        }   

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "App");
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {
                        //create the token
                        var claims = new[]
                        {
                            //new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            _config["Tokens:Issuer"],
                            _config["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddHours(4),
                            signingCredentials: creds
                            );

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            model = model
                        };

                        return Created("", results);
                    }
                }

            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(AuthenticationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.SignUpModel.UserName);

                if (user != null)
                {
                    ModelState.AddModelError("", "Username already exists!");
                    return View();
                }

                user = new StoreUser()
                {
                    FirstName = model.SignUpModel.FirstName,
                    LastName = model.SignUpModel.LastName,
                    UserName = model.SignUpModel.UserName,
                    Email = model.SignUpModel.Email
                };

                var result = await _userManager.CreateAsync(user, model.SignUpModel.Password);

                if (result == IdentityResult.Success)
                {
                    _ctx.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
            }
            ModelState.AddModelError("", "Failed to register!");
            return RedirectToAction("Login", "Account");

        }


    }
}
