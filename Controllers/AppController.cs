using DutchTreat.Data;
using DutchTreat.Models;
using DutchTreat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService mailService;
        private readonly IDutchRepository repository;

        public AppController(IMailService mailService, IDutchRepository repository)
        {
            this.mailService = mailService;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var results = repository.GetAllProducts();
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
                // Send the email     
                mailService.SendMessage("nasty_potapova@mail.ru", model.Subject, model.Message);
                ViewBag.UserMessage = "Mail sent";
                ModelState.Clear();
            }
            else
            {
                // Show the error
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        [Authorize]
        [HttpGet("shop")]
        public IActionResult Shop()
        {
            var results = repository.GetAllProducts();

            return View(results);
        }
    }
}
