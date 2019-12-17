﻿using Microsoft.AspNet.Identity;
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
            //int id = Convert.ToInt32(personId);
            Gallery gallery = db.Galleries.Find(id);
            return View(gallery);
        }

        public ActionResult New(int id)
        {
            Gallery newGallery = new Gallery();
            newGallery.UploadedBy = id;
            return View(newGallery);
        }

        [HttpPost]
        public ActionResult New(Gallery g)
        {
            try
            {
                db.Galleries.Add(g);

                Person person = db.People.Find(g.UploadedBy); // Referinta
                person.Galleries.Add(g);

                TempData["message"] = "Galeria cu numele " + g.GalleryName + " a fost adaugata!";
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}