using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BDT.Domain;
using BDT.Models;
using Stripe;

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

        public ActionResult Pay()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Pay(string stripeToken)
        {
            var myCharge = new StripeChargeCreateOptions();

            // always set these properties
            myCharge.AmountInCents = 5153;
            myCharge.Currency = "usd";

            // set this if you want to
            myCharge.Description = "Charge it like it's hot";

            // set this property if using a token
            myCharge.TokenId = stripeToken;

            var chargeService = new StripeChargeService();
            StripeCharge stripeCharge = chargeService.Create(myCharge);
            return View();
        }
    }
}
