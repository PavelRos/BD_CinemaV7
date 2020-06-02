using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BD_CinemaV7.Context;
using BD_CinemaV7.Models;

namespace BD_CinemaV7.Controllers
{
    public class placesController : Controller
    {
        private MsSqlContext db = new MsSqlContext();

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Index()
        {
            var places_list = db.places_list.Include(p => p.sessions).Include(p => p.User).Include(f=>f.sessions.film).Include(g=>g.sessions.hall).Where(c=>c.status=="Куплено");
          
            return View(await places_list.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            places places = await db.places_list.FindAsync(id);
            var ses = db.session.Where(c => c.Id == places.sessionsId).FirstOrDefault();
            var hall = db.halls.Where(c => c.Id == ses.hallId).FirstOrDefault();
            ViewBag.Hall = hall.hall_name;
            ViewBag.SesStartTime = ses.time_of_session;
            ViewBag.SesStartDate = ses.date_of_session;
            if (places == null)
            {
                return HttpNotFound();
            }
            return View(places);
        }
        public async Task<ActionResult> History()
        {
            var places_list = db.places_list.Include(p => p.sessions).Include(p => p.sessions.hall).Include(f => f.sessions.film).Include(g => g.sessions.hall).Include(f=>f.User).Where(c => c.User.Email ==User.Identity.Name);
            return View(await places_list.ToListAsync());
        }


       
        [HttpGet]
        public async Task<ActionResult> Index_Hall(int id)
        {
            var places_list = db.places_list.Include(p => p.sessions).Include(p => p.User).Where(c=>c.sessionsId==id);
            var ses = db.session.Where(c => c.Id == id).FirstOrDefault();
            var hall = db.halls.Where(c => c.Id == ses.hallId).FirstOrDefault();
            ViewBag.Hall = hall.hall_name;
            return View(await places_list.ToListAsync());
        }

       
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult> BuyTicket(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            places places = await db.places_list.FindAsync(id);
            var ses = db.session.Where(c => c.Id == places.sessionsId).FirstOrDefault();
            ViewBag.SesStartTime = ses.time_of_session;
            ViewBag.SesStartDate = ses.date_of_session;
            var hall = db.halls.Where(c => c.Id == ses.hallId).FirstOrDefault();
            ViewBag.Hall = hall.hall_name;
            if (places == null)
            {
                return HttpNotFound();
            }
            return View(places);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult> BuyTicket(int id)
        {
            if(User.Identity.IsAuthenticated==false)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                places places = await db.places_list.FindAsync(id);
                places.status = "Куплено";
                var user = db.Users.Where(c => c.Email == User.Identity.Name).FirstOrDefault();
                places.UserId = user.Id;
                DateTime data = new DateTime();
                data = DateTime.Now;
                places.date_of_operation = data;
                db.Entry<places>(places).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index","sessions");
            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
