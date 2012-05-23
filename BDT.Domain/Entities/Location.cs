using System.Collections.Generic;

namespace BDT.Domain.Entities
{
    public class Location : EntityBase
    {
        public int Seats { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Session> Session { get; set; }
    }

    public class Instructor : EntityBase
    {
        public string Name { get; set; }
    }
}