using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class TokenTable
    {
        public TokenTable(string token, DateTime expires)
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
