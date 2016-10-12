using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DailySports.DataLayer.Context;
using DailySports.DataLayer.Model;
using System.IO;

namespace DailySports.BackOffice.Controllers
{
    public class TournamentsController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: Tournaments
        public ActionResult Index()
        {
            var tournaments = db.Tournaments.Include(t => t.Game);
            return View(tournaments.ToList());
        }

        // GET: Tournaments/Create
        public ActionResult Create()
        {
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name");
            return View();
        }

        // POST: Tournaments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Format,Overview,MainEvent,Qualifiers,Description,URL,StartDate,EndDate,Price,GameId")] Tournaments tournaments, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var fileName = DateTime.Now.ToString("ddMMyyyyhhmmssffff") + "_" + Path.GetFileName(file.FileName);
                var virtualpath = "backend/Attachments/Images/" + "" + fileName;

                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Attachments/Images"), fileName);
                file.SaveAs(path);
                tournaments.TournamentImage = virtualpath;
                db.Tournaments.Add(tournaments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", tournaments.GameId);
            return View(tournaments);
        }

        // GET: Tournaments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournaments tournaments = db.Tournaments.Find(id);
            if (tournaments == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", tournaments.GameId);
            return View(tournaments);
        }

        // POST: Tournaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Format,Overview,MainEvent,Qualifiers,Description,URL,StartDate,EndDate,Price,GameId")] Tournaments tournaments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tournaments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", tournaments.GameId);
            return View(tournaments);
        }

        // GET: Tournaments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournaments tournaments = db.Tournaments.Find(id);
            if (tournaments == null)
            {
                return HttpNotFound();
            }
            return View(tournaments);
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tournaments tournaments = db.Tournaments.Find(id);
            db.Tournaments.Remove(tournaments);
            db.SaveChanges();
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
        public ActionResult Preview()
        {
            return Redirect("https://localhost:44319/Tournament/Index");
        }
    }
}
