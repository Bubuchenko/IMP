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

        public static async Task<Client> GetClient(string fingerprint)
        {
            using (IMPContext db = new IMPContext())
            {
                return await db.Clients.FirstOrDefaultAsync(f => f.Fingerprint == fingerprint);
            }
        }

        public static Task<bool> IsClientRegistered(string fingerprint)
        {
            using (IMPContext db = new IMPContext())
            {
                return db.Clients.Where(f => f.Fingerprint == fingerprint).AnyAsync();
            }
        }
        
    }
}
