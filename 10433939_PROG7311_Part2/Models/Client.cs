using System.ComponentModel.DataAnnotations;

namespace _10433939_PROG7311_Part2.Models
{
    public class Client
    {
        [Key]
        public int clientId { get; set; }
        public string name { get; set; }
        public string contractDetails { get; set; }
        public string region { get; set; }
    }
}
