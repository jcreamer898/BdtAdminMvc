using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BDT.Domain;
using BDT.Domain.Abstract;
using BDT.Domain.Entities;
using BDT.ViewModels;

namespace BDT.Controllers
{
    public class SessionsController : Controller
    {
        private ISessionRepository _sessionRepository;
        private ILocationRepository _locationRepository;
        private IInstructorRepostiory _instructorRepostiory;

        public SessionsController(ISessionRepository sessionRepository, ILocationRepository locationRepository, IInstructorRepostiory instructorRepository)
        {
            _sessionRepository = sessionRepository;
            _locationRepository = locationRepository;
            _instructorRepostiory = instructorRepository;
        }

        //
        // GET: /Sessions/

        public ActionResult Index()
        {
            return View(_sessionRepository.GetAllSession());
        }

        //
        // GET: /Sessions/Create

        public ActionResult Create()
        {
            var sessionViewModel = new SessionViewModel();
            sessionViewModel.Locations = _locationRepository.GetAllLocations()
                .Select(l => new SelectListItem
                {
                    Text = l.Name,
                    Value = l.Id.ToString()
                });

            sessionViewModel.Session = new Session();
            return View(sessionViewModel);
        } 

        //
        // POST: /Sessions/Create

        [HttpPost]
        public ActionResult Create(SessionViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                _sessionRepository.Add(viewModel.Session);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
        
        //
        // GET: /Sessions/Edit/5
 
        public ActionResult Edit(int id)
        {
            var session = _sessionRepository.Get(id);
            var sessionViewModel = new SessionViewModel();
            sessionViewModel.Locations = _locationRepository.GetAllLocations()
                .Select(loc => new SelectListItem
                {
                    Text = loc.Name,
                    Value = loc.Id.ToString(),
                    Selected = session.Locations.Any(l => l.Id == loc.Id)
                });
            sessionViewModel.Session = session;
            return View(sessionViewModel);
        }

        //
        // POST: /Sessions/Edit/5

        [HttpPost]
        public ActionResult Edit(SessionViewModel viewModel, string[] ids)
        {
//            if(ModelState.IsValid)
//            {
//                _sessionRepository.Update(viewModel.Session);
//                return RedirectToAction("Index");
//            }
            try
            {
                var context = new BdtContext();
                // Retrieve the item
                var item = _sessionRepository.Get(viewModel.Session.Id);
                // get the entry, so we can manipulate its state
                var itemEntry = context.Entry(item);
                // Make the entity modified
                itemEntry.State = EntityState.Modified;
                // Load the existing associated categories
                itemEntry.Collection(i => i.Locations).Load();
                // Remove all the existing associated categories
                item.Locations.Clear();

                // Retrieve the list of ids from the form submission (comes in comma-delimited string)
                string categories = string.Join(",", ids);
                // Split to an array
                var categoryIDs = categories.Split(',');
                // Iterate through each ID
                foreach (string locId in categoryIDs)
                {
                    int lid = int.Parse(locId);
                    // Add Items that aren't already present
                    item.Locations.Add(context.Locations.Find(lid));
                }

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(viewModel);
            }
        }

        //
        // GET: /Sessions/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Sessions/Delete/5

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _sessionRepository.Delete(id);
            return RedirectToAction("Index");
        }

        
        public ActionResult Dates(int id)
        {
            ViewBag.InstructorId = _instructorRepostiory.GetAllInstructors()
                .Select(inst => new SelectListItem
                    {
                        Text = inst.Name,
                        Value = inst.Id.ToString()
                    });
            return View(new SessionDate{ Date = DateTime.Now, SessionId = id});
        }
        
        [HttpPost]
        public ActionResult Dates(SessionDate sessionDate)
        {
            if(ModelState.IsValid)
            {
                _sessionRepository.AddDatesToSession(sessionDate.SessionId, new List<SessionDate> { sessionDate });
                return RedirectToAction("Edit", new { id = sessionDate.SessionId });
            }
            return View(sessionDate);
        }

        public ActionResult DeleteDate(int id, int sessionId)
        {
            _sessionRepository.DeleteDate(id);
            return RedirectToAction("Edit", new {id = sessionId});
        }

        public ActionResult QuickSessions()
        {
            return PartialView("_QuickSessions");
        }
    }
}
