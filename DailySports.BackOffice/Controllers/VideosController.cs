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

namespace DailySports.BackOffice.Controllers
{
    public class VideosController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: Videos
        public ActionResult Index()
        {
            var videos = db.Videos.Include(v => v.Category).Include(v => v.Game).Include(v => v.Tournament);
            return View(videos.ToList());
        }

        // GET: Videos/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name");
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name");
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,URL,Date,Tag,LiveStreamURL,GameId,CategoryId,TournamentId")] Videos videos)
        {
            if (ModelState.IsValid)
            {
                db.Videos.Add(videos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", videos.CategoryId);
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", videos.GameId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", videos.TournamentId);
            return View(videos);
        }

        // GET: Videos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Videos videos = db.Videos.Find(id);
            if (videos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", videos.CategoryId);
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", videos.GameId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", videos.TournamentId);
            return View(videos);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,URL,Date,Tag,LiveStreamURL,GameId,CategoryId,TournamentId")] Videos videos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", videos.CategoryId);
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", videos.GameId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", videos.TournamentId);
            return View(videos);
        }

        // GET: Videos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Videos videos = db.Videos.Find(id);
            if (videos == null)
            {
                return HttpNotFound();
            }
            return View(videos);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Videos videos = db.Videos.Find(id);
            db.Videos.Remove(videos);
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
            return Redirect("https://localhost:44319/Video/Index");
        }
    }
}
