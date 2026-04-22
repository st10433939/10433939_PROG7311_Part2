using _10433939_PROG7311_Part2.Data;
using _10433939_PROG7311_Part2.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _10433939_PROG7311_Part2.Controllers
{
    public class ContractsController : Controller
    {
        public readonly IWebHostEnvironment _environment;

        public ContractsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Index()
        {
            try
            {
                var contracts = ContractData.GetAllContracts();
                return View(contracts);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Unable to load contracts.";
                return View(new List<Contract>());
            }

        }

        public IActionResult Add()
        {
            return View();
        }

        //Post /Contracts/Add - Add form data to the datastore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Contract contract, List<IFormFile> documents, Client client)
        {
            try
            {
                if (string.IsNullOrEmpty(contract.Clients.name))
                {
                    ModelState.AddModelError("", "Client Required.");
                    return View(contract);
                }
                if (string.IsNullOrEmpty(contract.startDate.ToString()))
                {
                    ModelState.AddModelError("", "Start date Required.");
                    return View(contract);
                }
                if (string.IsNullOrEmpty(contract.endDate.ToString()))
                {
                    ModelState.AddModelError("", "End date Required.");
                    return View(contract);
                }
                if (string.IsNullOrEmpty(contract.serviceLevel))
                {
                    ModelState.AddModelError("", "Service level Required.");
                    return View(contract);
                }
                if (documents != null && documents.Count > 0)
                {
                    foreach (var file in documents)
                    {
                        if (file.Length > 0)
                        {
                            var allowedExtensions = new[] { ".pdf" };
                            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                            if (!allowedExtensions.Contains(extension))
                            {
                                ModelState.AddModelError("", $"File extension {extension} not allowed.");
                                return View(contract);
                            }

                            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                            Directory.CreateDirectory(uploadsFolder);


                            contract.Documents.Add(new UploadedDocument
                            {
                                FileName = file.FileName,
                                FilePath = "/uploads/",
                                FileSize = file.Length
                            });
                        }
                    }
                }

                ContractData.AddContract(contract);
                TempData["Success"] = "Contract added sucessfully";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"File extension {ex.Message} not allowed.");
                return View(contract);
            }
        }

        public IActionResult Details(int id)
        {
            try
            {
                var contract = ContractData.GetContractById(id);
                if (contract == null)
                {
                    TempData["Error"] = "contract not found.";
                    return View();
                }
                return View(contract);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error loading contract.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> DownloadDocument(int contractId, int docId)
        {
            try
            {
                var contract = ContractData.GetContractById(contractId);
                if (contract == null) { return NotFound("Contract not found."); }

                var document = contract.Documents.FirstOrDefault(doc => doc.Id == docId);
                if (document == null) { return NotFound("Document not found."); }

                var encryptedFilePath = Path.Combine(_environment.WebRootPath, document.FilePath.TrimStart('/'));
                if (!System.IO.File.Exists(encryptedFilePath)) return NotFound("File not found;");

                var contentType = Path.GetExtension(document.FileName).ToLower()
                    switch
                {
                    ".pdf" => "application/pdf"
                };

                return File(contentType, document.FileName);

            }
            catch (Exception ex)
            {
                return BadRequest("Error downloading file: " + ex.Message);
            }
        }

        // POST: /Admin/Approve - CREATES REVIEW RECORD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(int id)
        {
            try
            {
                var success = ContractData.UpdateContractStatus(id, ContractStatus.Active);

                if (success)
                {
                    TempData["Success"] = "Contract successfully active!";
                }
                else
                {
                    TempData["Error"] = "Contract not found.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error approving Contract.";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Review(int id)
        {
            try
            {
                var contract = ContractData.GetContractById(id);
                if (contract == null)
                {
                    TempData["Error"] = "Contract not found.";
                    return RedirectToAction(nameof(Index));
                }
                return View(contract);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error loading Contract.";
                return RedirectToAction(nameof(Index));
            }
        }
        // POST: /Contracts/Verified - CREATES REVIEW RECORD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Activate(int id)
        {
            try
            {
                var success = ContractData.UpdateContractStatus(id, ContractStatus.Active);

                if (success)
                {
                    TempData["Success"] = "Contract activated successfully!";
                }
                else
                {
                    TempData["Error"] = "Contract not found.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error activating Contract.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Hold(int id)
        {
            try
            {
                var success = ContractData.UpdateContractStatus(id, ContractStatus.OnHold);

                if (success)
                {
                    TempData["Success"] = "Contract successfully put on hold!";
                }
                else
                {
                    TempData["Error"] = "Contract not found.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error putting Contract on hold.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: /Contract/Decline - CREATES REVIEW RECORD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Decline(int id)
        {
            try
            {
                var success = ContractData.UpdateContractStatus(id, ContractStatus.Expired);

                if (success)
                {
                    TempData["Success"] = "Contract expired.";
                }
                else
                {
                    TempData["Error"] = "Contract not found.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error terminating Contract.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
