using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BDT.Domain.Abstract;
using BDT.Domain.Entities;

namespace BDT.Controllers
{
    public class InstructorController : Controller
    {
        private IInstructorRepostiory _repo;

        public InstructorController(IInstructorRepostiory repo)
        {
            _repo = repo;
        }

        //
        // GET: /Locations/

        public ActionResult Index()
        {
            return View(_repo.GetAllInstructors());
        }

        //
        // GET: /Locations/Create

        public ActionResult Create()
        {
            return View(new Instructor());
        } 

        //
        // POST: /Locations/Create

        [HttpPost]
        public ActionResult Create(Instructor instructor)
        {
            if(ModelState.IsValid)
            {
                _repo.Add(instructor);
                return RedirectToAction("Index");
            }

            return View(instructor);
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
        public ActionResult Edit(Instructor instructor)
        {
            if(ModelState.IsValid)
            {
                _repo.Update(instructor);
                return RedirectToAction("Index");
            }

            return View(instructor);
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
