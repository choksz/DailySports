using DailySports.DataLayer.Context;
using DailySports.DataLayer.Model;
using DailySports.ServiceLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DailySports.Backend.Controllers
{
    public class MatchesController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());

        // GET: Matches
        public IActionResult Index()
        {
            var matches = db.Matches.
                Include(m => m.TeamA).
                Include(m => m.TeamB).
                Include(m => m.Stage).
                ThenInclude(s => s.Tournament);
            return View(matches.ToList());
        }

        private SelectList GetStageSelectList()
        {
            var list = db.Stages.
                Join(db.Tournaments, st => st.TournamentId, t => t.Id, (st, t) => new { stage = st, tour = t }).
                Select(x => new { x.stage.Id, x.tour.Title, x.stage.Name }).ToList();
            var items = new List<SelectListItem>();
            foreach (var x in list)
            {
                items.Add(new SelectListItem
                {
                    Text = x.Title + " - " + x.Name,
                    Value = x.Id.ToString()
                });
            }
            return new SelectList(items, "Value", "Text");
        }

        private SelectList GetTeamSelectList()
        {
            var list = db.Teams.Join(db.Games, t => t.GameId, g => g.Id, (t, g) => new { team = t, game = g }).
                Select(x => new { Id = x.team.Id, Team = x.team.Name, Game = x.game.Name }).ToList();
            var items = new List<SelectListItem>();
            foreach (var x in list)
            {
                items.Add(new SelectListItem
                {
                    Text = x.Team + " (" + x.Game + ")",
                    Value = x.Id.ToString()
                });
            }
            return new SelectList(items, "Value", "Text");
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            var Teams = GetTeamSelectList();
            ViewBag.TeamAId = Teams;
            ViewBag.TeamBId = Teams;
            ViewBag.StageId = GetStageSelectList();
            return View(new Match());
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Match match)
        {
            if (ModelState.IsValid)
            {
                db.Matches.Add(match);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var Teams = GetTeamSelectList();
            ViewBag.TeamAId = Teams;
            ViewBag.TeamBId = Teams;
            ViewBag.StageId = GetStageSelectList();
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
            var Teams = GetTeamSelectList();
            ViewBag.TeamAId = Teams;
            ViewBag.TeamBId = Teams;
            ViewBag.StageId = GetStageSelectList();
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Match match)
        {
            if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var Teams = GetTeamSelectList();
            ViewBag.TeamAId = Teams;
            ViewBag.TeamBId = Teams;
            ViewBag.StageId = GetStageSelectList();
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
