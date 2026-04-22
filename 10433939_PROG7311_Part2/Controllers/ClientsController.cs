using _10433939_PROG7311_Part2.Data;
using _10433939_PROG7311_Part2.Models;
using Microsoft.AspNetCore.Mvc;

namespace _10433939_PROG7311_Part2.Controllers
{
    public class ClientsController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var clients = ClientData.GetAllClients();
                return View(clients);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Unable to load Clients.";
                return View(new List<Client>());
            }
        }

        public IActionResult AddClient()
        {
            return View();
        }

        //Post /Clients/AddClient
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClient(Client client)
        {
            try
            {
                if (string.IsNullOrEmpty(client.name))
                {
                    ViewBag.Error = "Client is required.";
                    return View(client);
                }
                if (string.IsNullOrEmpty(client.region))
                {
                    ViewBag.Error = "Client region required.";
                    return View(client);
                }

                ClientData.AddClient(client);
                TempData["Success"] = "Client added sucessfully";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag["Error"] = "Error handling Client" + ex.Message;
                return View(client);
            }
        }
    }
}
