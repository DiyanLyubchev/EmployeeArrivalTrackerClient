using EmployeeArrivalTrackerDomain.Adapter;
using EmployeeArrivalTrackerDomain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeArrivalTrackerClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeArrivalManager manager;
        public HomeController(IEmployeeArrivalManager manager)
        {;
            this.manager = manager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(this.manager.GetAllArrivalEmployees());
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            HttpContext.Session.TryGetValue("Error", out byte[] error);
            return View(ErrorAdapret.Transform(error));
        }
    }
}
