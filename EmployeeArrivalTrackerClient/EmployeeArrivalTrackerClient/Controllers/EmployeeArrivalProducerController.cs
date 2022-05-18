using EmployeeArrivalTrackerDomain.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeArrivalTrackerClient.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public class EmployeeArrivalProducerController : ControllerBase
    {
        private readonly IEmployeeArrivalManager dbManager;
        public EmployeeArrivalProducerController(IEmployeeArrivalManager dbManager)
        {
            this.dbManager = dbManager;
        }

        [HttpPost]
        public void Produce(object data)
        {
            string token = Request.Headers["X-Fourth-Token"];

            this.dbManager.AddArrivalAmployees(data, token);
        }
    }
}
