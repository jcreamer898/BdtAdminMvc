using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BDT.Domain.Abstract;
using BDT.Domain.Entities;

namespace BDT.Domain.Repositories
{
    public class LocationRepository : BaseRepository, ILocationRepository
    {
        public Location Get(int id)
        {
            return Db.Locations.SingleOrDefault(l => l.Id == id);
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return Db.Locations;
        } 

        public Location Add(Location location)
        {
            Db.Locations.Add(location);
            Db.SaveChanges();
            return location;
        }

        public Location Update(Location location)
        {
            Db.Entry(location).State = EntityState.Modified;
            Db.SaveChanges();
            return location;
        }

        public void Delete(int id)
        {
            Db.Locations.Remove(Get(id));
            Db.SaveChanges();
        }
    }
}
