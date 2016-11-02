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
    public class TeamMatchesController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: TeamMatches
        public ActionResult Index()
        {
            var teamMatches = db.TeamMatches.Include(t => t.Match).Include(t => t.Team);
            return View(teamMatches.ToList());
        }

        // GET: TeamMatches/Create
        public ActionResult Create()
        {
            ViewBag.MatchId = new SelectList(db.Matches, "Id", "Id");
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            return View();
        }

        // POST: TeamMatches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MatchId,TeamId")] TeamMatches teamMatches)
        {
            if (ModelState.IsValid)
            {
                db.TeamMatches.Add(teamMatches);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MatchId = new SelectList(db.Matches, "Id", "Id", teamMatches.MatchId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", teamMatches.TeamId);
            return View(teamMatches);
        }

        // GET: TeamMatches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMatches teamMatches = db.TeamMatches.Find(id);
            if (teamMatches == null)
            {
                return HttpNotFound();
            }
            ViewBag.MatchId = new SelectList(db.Matches, "Id", "Id", teamMatches.MatchId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", teamMatches.TeamId);
            return View(teamMatches);
        }

        // POST: TeamMatches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MatchId,TeamId")] TeamMatches teamMatches)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamMatches).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MatchId = new SelectList(db.Matches, "Id", "Id", teamMatches.MatchId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", teamMatches.TeamId);
            return View(teamMatches);
        }

        // GET: TeamMatches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMatches teamMatches = db.TeamMatches.Find(id);
            if (teamMatches == null)
            {
                return HttpNotFound();
            }
            return View(teamMatches);
        }

        // POST: TeamMatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamMatches teamMatches = db.TeamMatches.Find(id);
            db.TeamMatches.Remove(teamMatches);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(teamMatches);
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
