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
using System.Security.Principal;

namespace BD_CinemaV7.Controllers
{
    public class filmsController : Controller
    {
        private MsSqlContext db = new MsSqlContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.films.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            film film = await db.films.FindAsync(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            SelectList style = new SelectList(db.styles_list, "film_style", "film_style");
            ViewBag.styles = style;
            SelectList Age_raiting = new SelectList(db.age_rating_list, "Age_Rating", "Age_Rating");
            ViewBag.ages = Age_raiting;
            SelectList regiss = new SelectList(db.regs_list, "regisseur", "regisseur");
            ViewBag.regis = regiss;
            SelectList distrr = new SelectList(db.distr_list, "distributor", "distributor");
            ViewBag.distr = distrr;
            SelectList country = new SelectList(db.countries_list, "Country_Name", "Country_Name");
            ViewBag.country = country;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([Bind(Include = "Id,Country,budget,style,release_date,age_rating,duration,distributor,regisseur,film_name")] film film)
        {
            if (ModelState.IsValid)
            {
                db.films.Add(film);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(film);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            film film = await db.films.FindAsync(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            SelectList style = new SelectList(db.styles_list, "film_style", "film_style");
            ViewBag.styles = style;
            SelectList Age_raiting = new SelectList(db.age_rating_list, "Age_Rating", "Age_Rating");
            ViewBag.ages = Age_raiting;
            SelectList regiss = new SelectList(db.regs_list, "regisseur", "regisseur");
            ViewBag.regis = regiss;
            SelectList distrr = new SelectList(db.distr_list, "distributor", "distributor");
            ViewBag.distr = distrr;
            SelectList country = new SelectList(db.countries_list, "Country_Name", "Country_Name");
            ViewBag.country = country;
            return View(film);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Country,budget,style,release_date,age_rating,duration,distributor,regisseur,film_name")] film film)
        {
            if (ModelState.IsValid)
            {
                db.Entry(film).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(film);
        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            film film = await db.films.FindAsync(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            film film = await db.films.FindAsync(id);
            db.films.Remove(film);
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
