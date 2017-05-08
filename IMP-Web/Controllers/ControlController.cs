using IMP_Data;
using IMP_Data.Models;
using IMP_Data.Repositories;
using IMP_Lib.Models;
using IMP_Service;
using IMP_Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IMP_Web.Controllers
{
    public class ControlController : Controller
    {
        public async Task<ActionResult> Dashboard()
        {
            List<Client> clients = await ClientRepository.GetAllClients();

            await clients.SetOnlineStatuses();

            var dvm = new DashboardViewModel
            {
                Clients = clients
            };

            return View(dvm);
        }

        [HttpGet]
        public async Task<ActionResult> Client(string id)
        {
            Client client = await ClientRepository.GetClient(id);

            return View(client);
        }

        public ActionResult Clients()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Cmd(string id)
        {
            Client client = await ClientRepository.GetClient(id);
            return View(client);
        }
    }
}