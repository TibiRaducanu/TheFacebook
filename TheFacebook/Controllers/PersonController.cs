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

        public ActionResult Show(int id)
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"].ToString();
            }

            Person person = db.People.Find(id); // Showed profile
            string currentUser = User.Identity.GetUserId(); // Logged UserId

            ViewBag.People = db.People;
            ViewBag.CurrentUser = currentUser;
            ViewBag.UserMail = User.Identity.Name;
            var senderId = (from currPerson in db.People where (currPerson.UserId == currentUser) select currPerson.PersonId).FirstOrDefault();
            ViewBag.SenderId = senderId;
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated; // Check if a user is logged in

            ViewBag.Requests = person.Requests; // Requests of the showed profile
            ViewBag.MyProfile = person.UserId == currentUser; // Check if the profile showed is the user itself

            ViewBag.IsFriend = false;
            ViewBag.Pending = false;
            if(User.Identity.IsAuthenticated)
            {
                foreach(Friend friend in db.Friends)
                {
                    if(friend.FirstPersonId == id && friend.SecondPersonId == senderId || friend.FirstPersonId == senderId && friend.SecondPersonId == id)
                    {
                        ViewBag.IsFriend = true;
                    }
                }

                if (person.Requests != null && person.Requests.Any(x => x.UserId == currentUser))
                {
                    ViewBag.Pending = true;
                }
            }

            return View(person);
        }

        public ActionResult New()
        {
            Person newPerson = new Person();
            newPerson.UserId = User.Identity.GetUserId();
            MailBox newMailBox = new MailBox();
            newPerson.Mail = newMailBox;
            newPerson.Mail.UserId = User.Identity.GetUserId();
            db.MailBoxes.Add(newPerson.Mail);
            db.SaveChanges();
            return View(newPerson);
        }

        [HttpPost]
        public ActionResult New(Person p)
        {
            try
            {
                db.People.Add(p);
                db.SaveChanges();
                TempData["message"] = "Profilul cu numele " + p.Username + " a fost adaugat!" + "Profilul este " + p.PrivateUser;
                return RedirectToAction("Show", new { id = p.PersonId });
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AcceptFriendRequest(int receiverId, int senderId)
        {
            Person receiver = db.People.Find(receiverId);
            Person sender = db.People.Find(senderId);

            Friend relation = new Friend();
            relation.FirstPersonId = receiverId;
            relation.SecondPersonId = senderId;
            db.Friends.Add(relation);
            receiver.Requests.Remove(sender);
            sender.Requests.Remove(receiver);
            db.SaveChanges();
            return RedirectToAction("Show", new { id = receiverId });
        }

        [HttpPost]
        public ActionResult CancelFriendRequest(int receiverId, int senderId)
        {
            Person receiver = db.People.Find(receiverId);
            Person sender = db.People.Find(senderId);
            receiver.Requests.Remove(sender);
            db.SaveChanges();
            return RedirectToAction("Show", new { id = receiverId });
        }

        [HttpPost]
        public ActionResult SendFriendRequest(int receiverId, int senderId)
        {
            Person receiver = db.People.Find(receiverId);
            Person sender = db.People.Find(senderId);

            receiver.Requests.Add(sender);
            db.SaveChanges();
            return RedirectToAction("Show", new { id = receiverId });
        }
    }
}