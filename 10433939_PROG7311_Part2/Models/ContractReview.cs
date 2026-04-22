namespace _10433939_PROG7311_Part2.Models
{
    public class ContractReview
    {
        public int Id { get; set; }
        public int contractId { get; set; }
        public ContractStatus Decision { get; set; }
    }
}
