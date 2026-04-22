using _10433939_PROG7311_Part2.Models;

namespace _10433939_PROG7311_Part2.Data
{
    public class ContractData
    {
        private static List<Contract> _contracts = new List<Contract>() { };

        private static int _nextId = 4;
        private static int _nextReviewId = 1;

        public static List<Contract> GetAllContracts() => _contracts.ToList();

        public static Contract? GetContractById(int id) =>
            _contracts.FirstOrDefault(b => b.contractId == id);

        public static List<Contract> GetContractsByStatus(ContractStatus status) =>
            _contracts.Where(b => b.status == status).ToList();

        public static void AddContract(Contract contract)
        {
            contract.contractId = _nextId;
            _nextId++;
            contract.status = ContractStatus.Draft;
            _contracts.Add(contract);
        }

        public static bool UpdateContractStatus(int id, ContractStatus newStatus)
        {
            var contract = GetContractById(id);
            if (contract == null) return false;

            // CREATE REVIEW RECORD
            var review = new ContractReview
            {

                Id = _nextReviewId,
                contractId = id,
                Decision = newStatus,
            };
            _nextReviewId++;

            contract.Reviews.Add(review);

            // UPDATE Contract STATUS
            contract.status = newStatus;

            return true;
        }

        public static int GetDraftCount() =>
            _contracts.Count(b => b.status == ContractStatus.Draft);

        public static int GetOnHoldCount() =>
            _contracts.Count(b => b.status == ContractStatus.OnHold);

        public static int GetActiveCount() =>
            _contracts.Count(b => b.status == ContractStatus.Active);

        public static int GetExpiredCount() =>
            _contracts.Count(b => b.status == ContractStatus.Expired);
    }
}
