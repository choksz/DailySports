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
    public class TeamMatchesController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());



        // GET: TeamMatches
        public IActionResult Index()
        {
            var teamMatches = db.TeamMatches.Include(t => t.Match).Include(t => t.Team);
            return View(teamMatches.ToList());
        }

        // GET: TeamMatches/Create
        public IActionResult Create()
        {
            ViewBag.MatchId = new SelectList(db.Matches, "Id", "Id");
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            return View(new TeamMatches());
        }

        // POST: TeamMatches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,MatchId,TeamId")] TeamMatches teamMatches)
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
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            TeamMatches teamMatches = db.TeamMatches.Find(id);
            if (teamMatches == null)
            {
                return NotFound();
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
        public IActionResult Edit([Bind("Id,MatchId,TeamId")] TeamMatches teamMatches)
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            TeamMatches teamMatches = db.TeamMatches.Find(id);
            if (teamMatches == null)
            {
                return NotFound();
            }
            return View(teamMatches);
        }

        // POST: TeamMatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TeamMatches teamMatches = db.TeamMatches.Find(id);
            db.TeamMatches.Remove(teamMatches);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
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
