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
            List<MapViewClient> clients = await ClientRepository.GetAllClientsAsMapViewClients();

            foreach (MapViewClient client in clients)
            {
                if (await SessionRepository.ClientHasActiveSession(client.ID))
                    client.Status = "Online";
            }

            var dvm = new DashboardViewModel
            {
                Clients = clients
            };

            return View(dvm);
        }

        public ActionResult Clients()
        {
            return View();
        }
    }
}