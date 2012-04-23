using System.Data.Entity;
using BDT.Domain.Entities;

namespace BDT.Domain
{
    public class BdtContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionDate> SessionDates { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Location> Locations { get; set; }

        public BdtContext()
        {
            Configuration.ProxyCreationEnabled = false;
        }
    }
}