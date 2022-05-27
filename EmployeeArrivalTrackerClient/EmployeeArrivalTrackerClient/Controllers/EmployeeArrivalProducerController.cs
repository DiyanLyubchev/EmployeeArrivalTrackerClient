using Common.Models.Producer;
using EmployeeArrivalTrackerDomain.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeArrivalTrackerClient.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public class EmployeeArrivalProducerController : ControllerBase
    {
        private readonly IEmployeeArrivalManager manager;
        public EmployeeArrivalProducerController(IEmployeeArrivalManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        public IActionResult Produce(List<ProducerArrivalEmployeesVM> data)
        {
            string token = Request.Headers["X-Fourth-Token"];

            bool response = this.manager.AddArrivalEmployees(data, token);

            if (response)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
