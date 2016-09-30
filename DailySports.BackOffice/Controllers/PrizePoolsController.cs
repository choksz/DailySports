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
    public class PrizePoolsController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: PrizePools
        public ActionResult Index()
        {
            var prizePools = db.PrizePools.Include(p => p.Team).Include(p => p.Tournament);
            return View(prizePools.ToList());
        }

        // GET: PrizePools/Create
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View();
        }

        // POST: PrizePools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Prize,Level,TeamId,TournamentId")] PrizePool prizePool)
        {
            if (ModelState.IsValid)
            {
                db.PrizePools.Add(prizePool);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", prizePool.TeamId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", prizePool.TournamentId);
            return View(prizePool);
        }

        // GET: PrizePools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrizePool prizePool = db.PrizePools.Find(id);
            if (prizePool == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", prizePool.TeamId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", prizePool.TournamentId);
            return View(prizePool);
        }

        // POST: PrizePools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Prize,Level,TeamId,TournamentId")] PrizePool prizePool)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prizePool).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", prizePool.TeamId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", prizePool.TournamentId);
            return View(prizePool);
        }

        // GET: PrizePools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrizePool prizePool = db.PrizePools.Find(id);
            if (prizePool == null)
            {
                return HttpNotFound();
            }
            return View(prizePool);
        }

        // POST: PrizePools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrizePool prizePool = db.PrizePools.Find(id);
            db.PrizePools.Remove(prizePool);
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
    }
}
