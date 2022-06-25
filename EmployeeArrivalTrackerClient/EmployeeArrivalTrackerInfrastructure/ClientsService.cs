using EmployeeArrivalTrackerInfrastructure.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeArrivalTrackerInfrastructure
{
    public class ClientsService : IClientsService
    {
        private readonly ILogger<ClientsService> logger;

        public ClientsService(ILogger<ClientsService> logger)
        {
            this.logger = logger;
        }

        public async Task<string> CallCliensServiceAsync()
        {
            string responseMessage = string.Empty;
            using (var client = new HttpClient())
            {
                var date = DateTime.Now.ToString("yyyy-MM-dd");
                string url = new($"http://localhost:51397/api/clients/subscribe?date={date}&callback=https://localhost:5001/api/employeearrivalproducer/produce");
                Uri uri = new(url);

                client.DefaultRequestHeaders.Add("Accept-Client", "Fourth-Monitor");

                var responseMsg = await client.GetAsync(uri);

                if (responseMsg.IsSuccessStatusCode)
                {
                    responseMessage = await responseMsg.Content.ReadAsStringAsync();
                    return responseMessage;
                }
                else
                {
                    this.logger.LogError($"Service call was not successful! Message: {await responseMsg.Content.ReadAsStringAsync()} Status code: {(int)responseMsg.StatusCode}");
                }

                return responseMessage;
            };
        }
    }
}
