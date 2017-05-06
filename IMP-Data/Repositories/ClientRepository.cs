using IMP_Data.Models;
using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Data.Repositories
{
    public static class ClientRepository
    {
        public static async Task<bool> RegisterClient(Client client)
        {
            using (IMPContext db = new IMPContext())
            {
                client.CreationDate = DateTime.Now;
                db.Clients.Add(client);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public static async Task<Client> GetClient(string ClientId)
        {
            using (IMPContext db = new IMPContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                Client client = await db.Clients
                    .Include(f => f.PersonalInformation)
                    .Include(f => f.SystemInfo)
                    .Include(f => f.SystemInfo.Monitors)
                    .Include(f => f.SystemInfo.AntiVirus)
                    .Include(f => f.SystemInfo.Drives)
                    .Include(f => f.SystemInfo.InputDevices).FirstOrDefaultAsync(f => f.ClientId == ClientId);

                client.IsOnline = await SessionRepository.ClientHasActiveSession(ClientId);

                return client;
            }
        }

        public static async Task<List<Client>> GetAllClients()
        {
            using (IMPContext db = new IMPContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                List<Client> clients = await db.Clients
                    .Include(f => f.PersonalInformation)
                    .Include(f => f.SystemInfo)
                    .Include(f => f.SystemInfo.Monitors)
                    .Include(f => f.SystemInfo.AntiVirus)
                    .Include(f => f.SystemInfo.Drives)
                    .Include(f => f.SystemInfo.InputDevices).ToListAsync();

                return await clients.SetOnlineStatuses();
            }
        }

        public static async Task<bool> IsClientRegistered(string ClientId)
        {
            using (IMPContext db = new IMPContext())
            {
                return await db.Clients.Where(f => f.ClientId == ClientId).AnyAsync();
            }
        }
    }
}
