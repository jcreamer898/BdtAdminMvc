using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BDT.Domain.Entities;

namespace BDT.ViewModels
{
    public class SessionViewModel
    {
        public Session Session { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
    }
}