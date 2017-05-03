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
            try
            {
                using (IMPContext db = new IMPContext())
                {
                    client.CreationDate = DateTime.Now;
                    db.Clients.Add(client);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Exception ec = ex;
                return false;
            }
        }

        public static async Task<Client> GetClient(string ClientId)
        {
            using (IMPContext db = new IMPContext())
            {
                return await db.Clients.FirstOrDefaultAsync(f => f.ClientId == ClientId);
            }
        }

        public static async Task<bool> IsClientRegistered(string ClientId)
        {
            using (IMPContext db = new IMPContext())
            {
                return await db.Clients.Where(f => f.ClientId == ClientId).AnyAsync();
            }
        }

        public static async Task<List<MapViewClient>> GetAllClientsAsMapViewClients()
        {
            using (IMPContext db = new IMPContext())
            {
                List<Client> clients = await db.Clients.ToListAsync();

                List<MapViewClient> mapViewClients = new List<MapViewClient>();

                clients.ForEach(f => mapViewClients.Add(new MapViewClient
                {
                    ID = f.ClientId,
                    Location =
                    new float[] {
                        float.Parse(f.PersonalInformation.Location.Split(',')[0]),
                        float.Parse(f.PersonalInformation.Location.Split(',')[1]) },
                    Name = String.Format("{0} ({1})", f.Username, f.SystemInfo.MachineName)
                }));

                return mapViewClients;
            }
        }

    }
}
