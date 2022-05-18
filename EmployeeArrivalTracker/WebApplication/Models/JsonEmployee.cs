using System.Collections.Generic;

namespace WebApplication.Models
{
    public class JsonEmployee
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public int? ManagerId { get; set; }
        public int Id { get; set; }
        public List<string> Teams { get; set; }
    }
}
