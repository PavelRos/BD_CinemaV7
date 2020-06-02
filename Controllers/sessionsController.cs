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
    public class sessionsController : Controller
    {
        private MsSqlContext db = new MsSqlContext();


        
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var session = db.session.Include(s => s.film).Include(s => s.hall);

            return View(await session.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sessions sessions = await db.session.FindAsync(id);
            var film = db.films.Where(c => c.Id == sessions.filmId).FirstOrDefault();
            ViewBag.film_name = film.film_name;
            var hall = db.halls.Where(c => c.Id == sessions.hallId).FirstOrDefault();
            ViewBag.hall_name = hall.hall_name;
            if (sessions == null)
            {
                return HttpNotFound();
            }
            return View(sessions);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            SelectList films = new SelectList(db.films, "Id", "film_name");
            ViewBag.films = films;
            SelectList halls = new SelectList(db.halls, "Id", "hall_name");
            ViewBag.halls = halls;
            // ViewBag.filmId = new SelectList(db.films, "Id", "film_name");
            //  ViewBag.hallId = new SelectList(db.halls, "Id", "hall_name");

            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,date_of_session,filmname,time_of_session,hallname,hallId,filmId,price_of_tickets")] sessions sessions)
        {
            if (ModelState.IsValid)
            {
                var film = db.films.Where(c => c.Id == sessions.filmId).FirstOrDefault();

                if (DateTime.Parse(sessions.date_of_session) >= DateTime.Parse(film.release_date))
                {
                    string todaydate = DateTime.Today.ToString("yyyy.MM.dd");
                    int dd = DateTime.Parse(todaydate).CompareTo(DateTime.Parse(sessions.date_of_session));
                    if(dd<=0)
                    {
                        db.session.Add(sessions);
                        await db.SaveChangesAsync();
                        hall current_hall = db.halls
                           .Where(d => d.Id == sessions.hallId).FirstOrDefault();
                        film current_film = db.films
                            .Where(d => d.Id == sessions.filmId).FirstOrDefault();
                        int number_rows = current_hall.number_of_rows;
                        int number_seats = current_hall.number_of_seats_in_a_row;
                        for (int i = 1; i < number_rows + 1; i++)
                        {
                            for (int j = 1; j < number_seats + 1; j++)
                            {
                                places place = new places();
                                {
                                    place.sessionsId = sessions.Id;
                                    place.number_of_row = i;
                                    place.number_of_seat_in_a_row = j;
                                    place.status = "Свободно";

                                    //place.date_of_operation = DateTime.Now;
                                };

                                db.places_list.Add(place);
                                await db.SaveChangesAsync();
                            }

                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("date_of_session", "Неверная дата начала сеанса. Введите коррекную дату(Нельзя задать прошедший день)");
                    }
                   
                }
                else
                {
                    ModelState.AddModelError("date_of_session", "Неверная дата начала сеанса. Введите коррекную дату(позднее даты релиза фильма)");
                }
                   
            }
            ViewBag.filmId = new SelectList(db.films, "Id", "film_name", sessions.filmId);
            ViewBag.hallId = new SelectList(db.halls, "Id", "hall_name", sessions.hallId);
            return View(sessions);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sessions sessions = await db.session.FindAsync(id);
            if (sessions == null)
            {
                return HttpNotFound();
            }
            SelectList films = new SelectList(db.films, "Id", "film_name");
            ViewBag.films = films;
            SelectList halls = new SelectList(db.halls, "Id", "hall_name");
            ViewBag.halls = halls;
            return View(sessions);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,date_of_session,filmname,time_of_session,hallname,hallId,filmId,price_of_tickets")] sessions sessions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessions).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.filmId = new SelectList(db.films, "Id", "film_name", sessions.filmId);
            ViewBag.hallId = new SelectList(db.halls, "Id", "hall_name", sessions.hallId);
            return View(sessions);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sessions sessions = await db.session.FindAsync(id);
            var film = db.films.Where(c => c.Id == sessions.filmId).FirstOrDefault();
            ViewBag.film_name = film.film_name;
            var hall = db.halls.Where(c => c.Id == sessions.hallId).FirstOrDefault();
            ViewBag.hall_name = hall.hall_name;
            if (sessions == null)
            {
                return HttpNotFound();
            }
            return View(sessions);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            sessions sessions = await db.session.FindAsync(id);
            db.session.Remove(sessions);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
