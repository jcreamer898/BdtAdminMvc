using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BDT.Domain.Abstract;
using BDT.Domain.Entities;

namespace BDT.Controllers
{
    public class StudentsController : Controller
    {
        private IStudentRepository _repo;
        private ISessionRepository _sessionRepo { get; set; }

        public StudentsController(IStudentRepository repo, ISessionRepository sessionRepo)
        {
            _repo = repo;
            _sessionRepo = sessionRepo;
        }

        public ActionResult Index()
        {
            return View(_repo.GetAllStudents());
        }

        //
        // GET: /Students/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Students/Create

        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _repo.Create(student);
                return RedirectToAction("Index");
            }
                
           
            return View(student); 
        }
        
        //
        // GET: /Students/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(_repo.Get(id));
        }

        //
        // POST: /Students/Edit/5

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public ActionResult SignUp(int id)
        {
            ViewBag.Sessions = _sessionRepo.GetAllSession();
            return View(_repo.Get(id));
        }

        [HttpPost]
        public ActionResult SignUp(Student student)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        //
        // GET: /Students/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Students/Delete/5

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
    }
}
