using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TheFacebook.Models;

namespace TheFacebook.Controllers
{
    public class PersonController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Person
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"].ToString();
            }

            return View();
        }

        public ActionResult ShowUsers(string searchName)
        {
            var people = from p in db.People where p.Username == searchName select p;

            ViewBag.People = people;
            ViewBag.SearchedName = searchName;

            return View();
        }

        public ActionResult Show(int id)
        {
            //int id = Convert.ToInt32(personId);
            Person person = db.People.Find(id);
            return View(person);
        }

        public ActionResult New()
        {
            Person newPerson = new Person();
            newPerson.UserId = User.Identity.GetUserId();

            return View(newPerson);
        }

        [HttpPost]
        public ActionResult New(Person p)
        {
            try
            {
                db.People.Add(p);
                TempData["message"] = "Profilul cu numele " + p.Username + " a fost adaugat!";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}