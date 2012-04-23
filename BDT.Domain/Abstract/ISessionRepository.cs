using System.Collections.Generic;
using BDT.Domain.Entities;

namespace BDT.Domain.Abstract
{
    public interface ISessionRepository
    {
        Session Add(Session session);
        Session AddDatesToSession(int sessionId, IEnumerable<SessionDate> sessionDates);
        Session Get(int id);
        IEnumerable<Session> GetAllSession();
        Session Update(Session session);
        void Delete(int id);
        void DeleteDate(int id);
    }
}