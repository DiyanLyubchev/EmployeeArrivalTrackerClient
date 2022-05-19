using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class Tokens
    {
        public Tokens(string token, DateTime expires)
        {
            this.Token = token;
            this.Expires = expires;
        }

        [Key]
        public int Id { get; private set; }

        public string Token { get; private set; }

        public DateTime Expires { get; private set; }
    }
}
