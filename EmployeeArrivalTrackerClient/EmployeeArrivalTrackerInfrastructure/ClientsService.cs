using EmployeeArrivalTrackerInfrastructure.Contracts;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EmployeeArrivalTrackerInfrastructure
{
    public class ClientsService : IClientsService
    {
        public async Task<string> CallCliensServiceAsync()
        {
            string responseMessage = string.Empty;
            using (var client = new HttpClient())
            {
                var date = DateTime.Now.ToString("yyyy-MM-dd");
                string url = new($"http://localhost:51397/api/clients/subscribe?date={date}&callback=https://localhost:5001/api/employeearrivalproducer/produce");
                Uri uri = new(url);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Accept-Client", "Fourth-Monitor");
                client.DefaultRequestHeaders.Accept.Clear();

                var responseMsg = await client.GetAsync(uri);

                if (responseMsg.IsSuccessStatusCode)
                {
                    int responseStatusCode = (int)responseMsg.StatusCode;
                    responseMessage = await responseMsg.Content.ReadAsStringAsync();

                    return responseMessage;
                }

                return responseMessage;
            };
        }
    }
}
