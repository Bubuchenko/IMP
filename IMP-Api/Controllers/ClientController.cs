using IMP_Data;
using IMP_Data.Extensions;
using IMP_Data.Repositories;
using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace IMP_Api.Controllers
{
    public class ClientController : ApiController
    {

        [HttpGet]
        public async Task<IHttpActionResult> FindBy1(string antivirusstatus, string status, string systemtype)
        {
            IEnumerable<Client> clients = await ClientRepository.GetAllClients();

            bool isOnline = status == "Online" ? true : false;

            if (antivirusstatus != null)
                clients = clients.Where(f => f.SystemInfo.AntiVirus.ProductName == antivirusstatus);
            if (status != null)
                clients = clients.Where(f => f.IsOnline == isOnline);
            if (systemtype != null)
                clients = clients.Where(f => f.SystemInfo.SystemType == systemtype);



            return Ok(clients.ToList().Serialize());
        }

        [HttpGet]
        public async Task<IHttpActionResult> Status(string parameter)
        {
            bool online = parameter == "Online" ? true : false;

            List<Client> clients = await ClientRepository.GetAllClients();
            string SerializedClients = clients.Where(f => f.IsOnline == online).ToList().Serialize();
            return Ok(SerializedClients);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Antivirus(string parameter)
        {
            List<Client> clients = await ClientRepository.GetAllClients();
            string SerializedClients = clients.Where(f => f.SystemInfo.AntiVirus.ProductName == parameter).ToList().Serialize();
            return Ok(SerializedClients);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
    }
}