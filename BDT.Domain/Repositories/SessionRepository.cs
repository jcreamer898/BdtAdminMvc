using System.Collections.Generic;
using System.Data;
using System.Linq;
using BDT.Domain.Abstract;
using BDT.Domain.Entities;

namespace BDT.Domain.Repositories
{
    /// <summary>
    /// A repository for working with Sessions
    /// </summary>
    public class SessionRepository : BaseRepository, ISessionRepository
    {
        /// <summary>
        /// Add a new session
        /// </summary>
        /// <param name="session">The session to add</param>
        /// <returns>The added session</returns>
        public Session Add(Session session)
        {
            Db.Sessions.Add(session);
            Db.SaveChanges();
            return session;
        }

        /// <summary>
        /// Add new dates to an already existing session
        /// </summary>
        /// <param name="sessionId">The Session Id to add the dates</param>
        /// <param name="sessionDates">The dates to add</param>
        /// <returns>The Session the dates were added to</returns>
        public Session AddDatesToSession(int sessionId, IEnumerable<SessionDate> sessionDates)
        {
            var session = Db.Sessions.SingleOrDefault(s => s.Id == sessionId);
            sessionDates.ToList().ForEach(sd =>
                {
                    sd.SessionId = session.Id;
                    Db.SessionDates.Add(sd);
                });
            Db.SaveChanges();
            return session;
        }

        /// <summary>
        /// Retrieve a session and its dates
        /// </summary>
        /// <param name="id">The id of the session to retrieve</param>
        /// <returns></returns>
        public Session Get(int id)
        {
            return Db.Sessions.Include("SessionDates")
                .Include("SessionDates.Instructor")
                .Include("Locations")
                .SingleOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// Retreive all sessions
        /// </summary>
        /// <returns>A list of sessions</returns>
        public IEnumerable<Session> GetAllSession()
        {
            return Db.Sessions.Include("Locations");
        }

        public Session Update(Session session)
        {
            Db.Entry(session).State = EntityState.Modified;
            Db.SaveChanges();
            return session;
        }

        public void Delete(int id)
        {
            Db.Sessions.Remove(Get(id));
            Db.SaveChanges();
        }

        public void DeleteDate(int id)
        {
            var sessionDate = Db.SessionDates.SingleOrDefault(d => d.Id == id);
            Db.SessionDates.Remove(sessionDate);
            Db.SaveChanges();
        }
    }
}