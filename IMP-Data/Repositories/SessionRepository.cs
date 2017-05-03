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
    public static class SessionRepository
    {

        public static async Task<List<Session>> GetActiveSessions()
        {
            using (IMPContext db = new IMPContext())
            {
                return await db.Sessions.Where(f => f.SessionEnd != null).ToListAsync();
            }
        }

        public static async Task CreateSession(Session session)
        {
            using (IMPContext db = new IMPContext())
            {
                try
                {
                    db.Sessions.Add(session);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static async Task EndSession(string SessionID)
        {
            using (IMPContext db = new IMPContext())
            {
                db.Sessions.OrderByDescending(f => f.Id).FirstOrDefaultAsync(f => f.SessionID == SessionID).Result.SessionEnd = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }

        public static async Task<bool> ClientHasActiveSession(string ClientID)
        {
            using (IMPContext db = new IMPContext())
            {
                Session session = await GetClientSession(ClientID);

                if (session == null)
                    return false;

                return (session.SessionEnd == null);
            }
        }

        public static async Task<Session> GetClientSession(string ClientID)
        {
            using (IMPContext db = new IMPContext())
            {
                return await db.Sessions.OrderByDescending(f => f.Id).FirstOrDefaultAsync(f => f.ClientID == ClientID);
            }
        }
    }
}
