using System.Collections.Generic;
using BDT.Domain.Entities;

namespace BDT.Domain.Abstract
{
    public interface ILocationRepository
    {
        Location Get(int id);
        IEnumerable<Location> GetAllLocations();
        Location Add(Location location);
        Location Update(Location location);
        void Delete(int location);
    }
}