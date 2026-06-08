using System.ComponentModel.DataAnnotations;

namespace POE_P3_API
{
    public class Contracts
    {
        [Key]
        public int contractId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string status { get; set; }
        public string serviceLevel { get; set; }
    }
}
