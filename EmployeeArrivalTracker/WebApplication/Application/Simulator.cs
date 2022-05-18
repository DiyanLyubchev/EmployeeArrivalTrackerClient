using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Application
{
    public class Simulator
    {
        private readonly IList<JsonEmployee> _employees;

        private readonly Client _client;

        public Simulator(Client client, string rootPath)
        {
            _client = client;
            _employees = JsonConvert.DeserializeObject<IList<JsonEmployee>>(new StreamReader(
                 Path.Combine(rootPath, "bin/debug/net5.0/employees.json")).ReadToEnd());
        }

        public void Simulate(DateTime when)
        {
            Task.Factory.StartNew(async () =>
            {
                bool stop = false;
                while (!stop)
                {
                    var random = new Random(Environment.TickCount);
                    Thread.Sleep(1000 * random.Next(1, 50)); //Wait between 1 and 50 secs

                    var data = SimulateData(when);
                    if (!data.Any())
                    {
                        //Finished simulating today's data
                        stop = true;
                    }
                    int count = 10;
                    while (count > 0)
                        try
                        {
                            await _client
                                    .Url
                                    .WithHeader("X-Fourth-Token", _client.Token)
                                    .PostJsonAsync(data);

                            count = 0;
                        }
                        catch (Exception ex)
                        {
                            --count;
                            Thread.Sleep(1000);
                        }
                }
            });
        }

        readonly Dictionary<int, JsonEmployee> _simulated = new Dictionary<int, JsonEmployee>();
        readonly object _locker = new object();

        private IList<SimulationData> SimulateData(DateTime when)
        {
            //So we dont overlap requests
            lock (_locker)
            {
                var random = new Random(Environment.TickCount);
                var count = random.Next(5, 101); //from 5 to 100 employees
                var employees =
                    _employees.Where(x => !_simulated.ContainsKey(x.Id))
                        .OrderBy(x => Guid.NewGuid())
                        .Take(count)
                        .ToList();
                foreach (var e in employees)
                {
                    _simulated.Add(e.Id, e);
                }
                return employees.Select(x =>
                    new SimulationData
                    {
                        EmployeeId = x.Id,
                        When = when.AddHours(8).AddMinutes(random.Next(120)).ToString("u").Replace(" ", "T"),
                    }).ToList();
            }
        }
    }
}
