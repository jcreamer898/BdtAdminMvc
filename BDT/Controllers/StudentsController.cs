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

        public StudentsController(IStudentRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            return View(_repo.GetAllStudents());
        }

        //
        // GET: /Students/Details/5

        public ActionResult Details(int id)
        {
            return View();
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
                _repo.AddNew(student);

                return RedirectToAction("Index");
            }
                
           
            return View(student); 
        }
        
        //
        // GET: /Students/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Students/Edit/5

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
