using EmployeeArrivalTrackerClient.Models;
using EmployeeArrivalTrackerDomain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace EmployeeArrivalTrackerClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IEmployeeArrivalManager manager;
        public HomeController(ILogger<HomeController> logger, IEmployeeArrivalManager manager)
        {
            this.logger = logger;
            this.manager = manager;
        }

        public IActionResult Index()
        {
            return View(this.manager.GetAllArrivalEmployees());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
