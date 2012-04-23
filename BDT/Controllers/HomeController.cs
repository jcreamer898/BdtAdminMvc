using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BDT.Domain;
using BDT.Models;

namespace BDT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to BDT Admin!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
