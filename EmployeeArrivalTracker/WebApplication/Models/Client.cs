using System;

namespace WebApplication.Models
{
    public class Client
    {
        public Client(string callback, string token)
        {
            Url = new Uri(callback);
            Token = token;
        }

        public Uri Url { get; private set; }
        public string Token { get; private set; }
    }
}
