using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheFacebook.Models;

namespace TheFacebook.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searchString)
        {
            var people = from p in db.People where p.username == searchString select p;
            ViewBag.People = people;
            ViewBag.SearchedName = searchString;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NewPerson()
        {
            Person p = new Person();

            return View(p);
        }

        [HttpPost]
        public ActionResult NewPerson(Person p)
        {
            try
            {
                db.People.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }
        } 
    }
}