using IMP_Data;
using IMP_Data.Enums;
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
        public async Task<IHttpActionResult> FindByAntivirusStatusSystemTypeCreationDateOS(string antivirusstatus, string antivirusname, string status, string systemtype, string creationdate, string operatingsystem)
        {

            IEnumerable<Client> clients = await ClientRepository.GetAllClients();

            AntivirusStatus antivirusStatus;
            DateTime creationDate;

            bool isOnline = status == "Online" ? true : false;
            bool isValidAntivirusStatus = Enum.TryParse<AntivirusStatus>(antivirusstatus, true, out antivirusStatus);
            bool isValidCreationDate = DateTime.TryParse(creationdate, out creationDate);


            if (!string.IsNullOrEmpty(antivirusstatus) && isValidAntivirusStatus == true)
                clients = clients.Where(f => f.SystemInfo.AntiVirus.FirstOrDefault().GetAntivirusState()
                .Status == antivirusStatus);
            if (!string.IsNullOrEmpty(creationdate) && isValidCreationDate)
                clients = clients.Where(f => f.CreationDate >= creationDate);
            if (!string.IsNullOrEmpty(antivirusname))
                clients = clients.Where(f => f.SystemInfo.AntiVirus.FirstOrDefault().ProductName == antivirusname);
            if (!string.IsNullOrEmpty(status))
                clients = clients.Where(f => f.IsOnline == isOnline);
            if (!string.IsNullOrEmpty(systemtype))
                clients = clients.Where(f => f.SystemInfo.SystemType == systemtype);
            if (!string.IsNullOrEmpty(operatingsystem))
                clients = clients.Where(f => f.SystemInfo.OperatingSystem == operatingsystem.TrimEnd(' '));


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
        public async Task<IHttpActionResult> Id(string parameter)
        {
            Client client = await ClientRepository.GetClient(parameter);
            return Ok(client.Serialize());
        }
    }
}