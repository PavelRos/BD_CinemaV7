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
    public class hallsController : Controller
    {
        private MsSqlContext db = new MsSqlContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.halls.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hall hall = await db.halls.FindAsync(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            return View(hall);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            SelectList types = new SelectList(db.types, "Hall_Type", "Hall_Type");
            ViewBag.Types = types;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([Bind(Include = "hall_name,type,number_of_rows,number_of_seats_in_a_row")] hall hall)
        {
            if (ModelState.IsValid)
            {
                db.halls.Add(hall);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hall);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hall hall = await db.halls.FindAsync(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            SelectList types = new SelectList(db.types, "Hall_Type", "Hall_Type");
            ViewBag.Types = types;
            return View(hall);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,hall_name,type,number_of_rows,number_of_seats_in_a_row")] hall hall)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hall).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hall);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hall hall = await db.halls.FindAsync(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            return View(hall);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            hall hall = await db.halls.FindAsync(id);
            db.halls.Remove(hall);
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
