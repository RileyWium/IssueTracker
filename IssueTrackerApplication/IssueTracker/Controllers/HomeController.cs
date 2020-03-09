using IssueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace IssueTracker.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            ViewBag.Message = "Project List";
           
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Description page.";

            return View();
        }
    }
}