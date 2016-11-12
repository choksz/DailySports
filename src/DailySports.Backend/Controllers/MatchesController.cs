using DailySports.DataLayer.Context;
using DailySports.DataLayer.Model;
using DailySports.ServiceLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DailySports.Backend.Controllers
{
    public class MatchesController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());



        // GET: Matches
        public IActionResult Index()
        {
            var matches = db.Matches.Include(m => m.TeamA).Include(m => m.TeamB).Include(m => m.Tournament);
            return View(matches.ToList());
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewBag.TeamAId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.TeamBId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View(new Match());
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Date,TournamentId,TeamAId,TeamBId")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Matches.Add(match);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamAId = new SelectList(db.Teams, "Id", "Name", match.TeamAId);
            ViewBag.TeamBId = new SelectList(db.Teams, "Id", "Name", match.TeamBId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", match.TournamentId);
            return View(match);
        }

        // GET: Matches/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewBag.TeamAId = new SelectList(db.Teams, "Id", "Name", match.TeamAId);
            ViewBag.TeamBId = new SelectList(db.Teams, "Id", "Name", match.TeamBId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", match.TournamentId);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Date,TournamentId,TeamAId,TeamBId")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamAId = new SelectList(db.Teams, "Id", "Name", match.TeamAId);
            ViewBag.TeamBId = new SelectList(db.Teams, "Id", "Name", match.TeamBId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", match.TournamentId);
            return View(match);
        }

        // GET: Matches/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Match match = db.Matches.Find(id);
            db.Matches.Remove(match);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(match);
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
