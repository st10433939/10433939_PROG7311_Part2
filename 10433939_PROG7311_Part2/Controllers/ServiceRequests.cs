using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using _10433939_PROG7311_Part2.Data;
using _10433939_PROG7311_Part2.Models;


namespace _10433939_PROG7311_Part2.Controllers
{
    public class ServiceRequests : Controller
    {
        public IActionResult Index(string filter = "all")
        {
            try
            {
                var contracts = ContractData.GetAllContracts();
                ViewBag.Filter = filter;

                contracts = filter.ToLower()
                    switch
                {
                    "active" => ContractData.GetContractsByStatus(ContractStatus.Active),
                    _ => contracts
                };

                ViewBag.ActiveCount = ContractData.GetActiveCount();

                return View(contracts);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Unable to load Contracts.";
                return View(new List<Contract>());
            }

            return View();
        }


    }
}
