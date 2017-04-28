using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Data.Repositories
{
    public class ClientRepository
    {
        public async Task RegisterClient(Client client)
        {
            using (IMPContext db = new IMPContext())
            {
                db.Clients.Add(client);
                await db.SaveChangesAsync();
            }
        }

        public bool IsClientRegistered(string fingerprint)
        {
            using (IMPContext db = new IMPContext())
            {
                return db.Clients.Where(f => f.Fingerprint == fingerprint).Any();
            }
        }

    }
}
