using System;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Application;
using WebApplication.Extentions;
using WebApplication.Models;

namespace WebService.Controllers
{
    public class ClientsController : ControllerBase
    {
        private string token = Guid.NewGuid().ToString("N");
        private readonly IWebHostEnvironment _env;

        public ClientsController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [Route("api/clients/subscribe")]
        public IActionResult Get(string date, string callback)
        {
            var headerValue = Request.GetHeader("Accept-Client");
            if(headerValue != "Fourth-Monitor")
                return Unauthorized();

            var newClient = new Client()
            {
                Url = new Uri(callback),
                Token = token
            };

            new Simulator(newClient, _env.ContentRootPath).Simulate(DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.CurrentCulture));
            return Ok(new { newClient.Token, Expires = DateTime.UtcNow.AddHours(8).ToString("u").Replace(" ", "T") });
        }
    } 
}
