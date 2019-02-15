using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Services;
using OnlineStore.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStore.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IStoreRepository _repository;

        public AppController(IMailService mailService, IStoreRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }
          
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {

            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //send the email
                _mailService.SendMessage("bb@yahoo.com", model.Subject, $"From: {model.Name} - {model.Email}, Review: {model.Review}");
                ViewBag.UserMessage = "Mesaj trimis";
                ModelState.Clear();
            }

            return View();
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            ViewBag.Title = "Despre noi";

            return View();
        }

        public IActionResult Shop()
        {
            return View();
        }
    }
}
