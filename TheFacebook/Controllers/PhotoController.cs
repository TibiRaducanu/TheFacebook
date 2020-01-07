using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheFacebook.Models;

namespace TheFacebook.Controllers
{
    public class PhotoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"].ToString();
            }

            return View();
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            Photo photo = db.Photos.Find(id);
            string userId = User.Identity.GetUserId();
            ViewBag.UserName = (from person in db.People where (person.UserId == userId) select person.Username).FirstOrDefault();
            ViewBag.People = db.People;

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"].ToString();
            }

            ViewBag.currentUser = User.Identity.GetUserId();
            ViewBag.isAdmin = User.IsInRole("Administrator");

            return View(photo);
        }

        [HttpGet]
        public ActionResult New(int id)
        {
            Photo newPhoto = new Photo();
            newPhoto.GalleryId = id;

            return View(newPhoto);
        }

        [HttpPost]
        public ActionResult New(Photo p, HttpPostedFileBase upload)
        {
            try
            {
                if(upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        byte[] content = reader.ReadBytes(upload.ContentLength);
                        string img = Convert.ToBase64String(content);
                        p.Image = img;
                    }
                }

                db.Photos.Add(p);
                Gallery gallery = db.Galleries.Find(p.GalleryId); // Referinta
                gallery.Photos.Add(p);

                TempData["message"] = "Poza cu numele " + p.PhotoName + " a fost adaugata!";
                db.SaveChanges();

                return RedirectToAction("Show", "Gallery", new { id = p.GalleryId });
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
                return RedirectToAction("");
            }
        }

        [HttpDelete]
        public ActionResult Delete(int galleryId, int photoId)
        {
            Gallery g = db.Galleries.Find(galleryId);
            Photo photoToDelete = db.Photos.Find(photoId);
            Person p = db.People.Find(g.PersonId);

            if (p.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
            {
                //p.Galleries.Remove(galleryToDelete);
                g.Photos.Remove(photoToDelete);
                db.Photos.Remove(photoToDelete);
                db.SaveChanges();
                TempData["message"] = "Poza a fost stearsa cu succes!";
                return RedirectToAction("Show", "Gallery", new { id = galleryId });
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti o poza care nu va apartine!";
                return RedirectToAction("Show", "Gallery", new { id = galleryId });
            }
        }
    }
}