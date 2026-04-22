using System.ComponentModel.DataAnnotations;

namespace _10433939_PROG7311_Part2.Models
{
    public class ServiceRequest
    {
        [Key]
        public int serviceId { get; set; }
        public string description { get; set; }
        public double cost { get; set; }
        public string status { get; set; }

        //public Contract? Contracts { get; set; }
        public List<Contract> Contracts { get; set; }
    }
}
