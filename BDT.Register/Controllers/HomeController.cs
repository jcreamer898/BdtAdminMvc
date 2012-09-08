using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BDT.Domain;
using BDT.Domain.Entities;
using BDT.Domain.Repositories;

namespace BDT.Register.Controllers
{
    public class HomeController : Controller
    {
        private BdtContext _db;
        //
        // GET: /Home/

        public HomeController()
        {
            _db = new BdtContext();
        }

        public ActionResult Index()
        {
            var students = new List<Student>();   
            return View(students);
        }

        [HttpPost]
        public ViewResult Index(string email, string zip)
        {
            return View(_db .Students.Where(s => s.Email == email && s.Zip == zip));
        }



        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SignUp(int id)
        {
            var student = _db.Students.SingleOrDefault(s => s.Id == id);

            ViewBag.LocationId = _db.Locations
                .ToList()
                .Select(inst => new SelectListItem
                {
                    Text = inst.Name,
                    Value = inst.Id.ToString()
                });
            ViewBag.SessionId = _db.Sessions
                .ToList()
                .Select(inst => new SelectListItem
                {
                    Text = inst.Name,
                    Value = inst.Id.ToString()
                });
            return View(student);
        }

        [HttpPost]
        public ActionResult SignUp(Student student, int sessionId, int locationId)
        {
            try
            {
                var studentRepository = new StudentRepository();
                var session = _db.Sessions.SingleOrDefault(s => s.Id == sessionId);
                var location = _db.Locations.SingleOrDefault(s => s.Id == locationId);
                studentRepository.SignUp(student.Id, session.Id);
            }
            catch (Exception e)
            {
                var st = _db.Students.SingleOrDefault(s => s.Id == student.Id);
                ViewBag.LocationId = _db.Locations
                .ToList()
                .Select(inst => new SelectListItem
                {
                    Text = inst.Name,
                    Value = inst.Id.ToString()
                });
                ViewBag.SessionId = _db.Sessions
                    .ToList()
                    .Select(inst => new SelectListItem
                    {
                        Text = inst.Name,
                        Value = inst.Id.ToString()
                    });

                ModelState.AddModelError("", e.Message);
                return View(student);
            }
            return RedirectToAction("Index");
        }
    }
}
