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
    public class GalleryController : Controller
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

        public ActionResult Show(int id)
        {
            Gallery gallery = db.Galleries.Find(id);
            string myUser = User.Identity.GetUserId();
            int myId = (from person in db.People where (person.UserId == myUser) select person.PersonId).FirstOrDefault();
            ViewBag.MyProfile = gallery.PersonId == myId;
            return View(gallery);
        }

        public ActionResult New(int id)
        {
            Gallery newGallery = new Gallery();
            newGallery.PersonId = id;
            return View(newGallery);
        }

        [HttpPost]
        public ActionResult New(Gallery g)
        {
            try
            {
                db.Galleries.Add(g);

                Person person = db.People.Find(g.PersonId); // Referinta
                person.Galleries.Add(g);

                TempData["message"] = "Galeria cu numele " + g.GalleryName + " a fost adaugata!";
                db.SaveChanges();

                return RedirectToAction("Show", "Person", new { id = person.PersonId});
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
                return RedirectToAction("");
            }
        }
    }
}