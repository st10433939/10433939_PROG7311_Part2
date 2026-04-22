using System.ComponentModel.DataAnnotations;

namespace _10433939_PROG7311_Part2.Models
{
    public class Contract
    {
        [Key]
        public int contractId { get; set; }
        public DateTime startDate {  get; set; }
        public DateTime endDate { get; set; }
        public ContractStatus status { get; set; }
        public string serviceLevel { get; set; }
        public List<UploadedDocument> Documents { get; set; }
        public Client? Clients { get; set; }
        public ServiceRequest? Requests { get; set; }
        public List<ContractReview> Reviews { get; set; } = new List<ContractReview>();
    }
}
