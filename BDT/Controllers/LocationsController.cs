using System.Web.Mvc;
using BDT.Domain.Abstract;
using BDT.Domain.Entities;

namespace BDT.Controllers
{
    public class LocationsController : Controller
    {
        private ILocationRepository _repo;

        public LocationsController(ILocationRepository repo)
        {
            _repo = repo;
        }

        //
        // GET: /Locations/

        public ActionResult Index()
        {
            return View(_repo.GetAllLocations());
        }

        //
        // GET: /Locations/Create

        public ActionResult Create()
        {
            return View(new Location());
        } 

        //
        // POST: /Locations/Create

        [HttpPost]
        public ActionResult Create(Location location)
        {
            if(ModelState.IsValid)
            {
                _repo.Add(location);
                return RedirectToAction("Index");
            }

            return View(location);
        }
        
        //
        // GET: /Locations/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(_repo.Get(id));
        }

        //
        // POST: /Locations/Edit/5

        [HttpPost]
        public ActionResult Edit(Location location)
        {
            if(ModelState.IsValid)
            {
                _repo.Update(location);
                return RedirectToAction("Index");
            }

            return View(location);
        }

        //
        // GET: /Locations/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_repo.Get(id));
        }

        //
        // POST: /Locations/Delete/5

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
