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
            catch
            {
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

        public static async Task SetLastSeenDate(string ClientId, DateTime LastOnline)
        {
            using (IMPContext db = new IMPContext())
            {
                db.Clients.FirstOrDefault(f => f.ClientId == ClientId).LastOnline = LastOnline;
                await db.SaveChangesAsync();
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
