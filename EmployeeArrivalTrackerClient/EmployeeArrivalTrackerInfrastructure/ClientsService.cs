using Common;
using Common.Options;
using EmployeeArrivalTrackerInfrastructure.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeArrivalTrackerInfrastructure
{
    public class ClientsService : IClientsService
    {
        private readonly ILogger<ClientsService> logger;
        private readonly InfrastructureOptions options;
        public ClientsService(ILogger<ClientsService> logger, IOptions<InfrastructureOptions> options)
        {
            this.logger = logger;
            this.options = options.Value;
        }

        public async Task<string> CallCliensServiceAsync()
        {
            string responseMessage = string.Empty;
            using (var client = new HttpClient())
            {
                var date = DateTime.Now.ToString("yyyy-MM-dd");
                string url = new($"{this.options.TrackerApiUrl}{date}&callback={this.options.CallBackUrl}");
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
                    this.logger.LogError($"Service call was not successful! Env: {Utils.GetCurrentEnvironment()} Message: {await responseMsg.Content.ReadAsStringAsync()} Status code: {(int)responseMsg.StatusCode}");
                }

                return responseMessage;
            };
        }
    }
}
