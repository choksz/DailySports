using DailySports.DataLayer.Context;
using DailySports.DataLayer.Model;
using DailySports.ServiceLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace DailySports.Backend.Controllers
{
    public class TeamListsController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());

        // GET: teamLists
        public IActionResult Index()
        {
            var teamLists = db.TeamLists.Include(g => g.Tournament);
            return View(teamLists.ToList());
        }

        // GET: teamLists/Create
        public IActionResult Create()
        {
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View(new TeamList());
        }

        // POST: teamLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeamList teamList)
        {
            if (ModelState.IsValid)
            {
                db.TeamLists.Add(teamList);
                int id = db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", teamList.TournamentId);
            return View(teamList);
        }

        private IDictionary<int, Tuple<Team, bool>> GetTeams(TeamList teamList)
        {
            var TeamListTeams = db.TeamListTeams.Where(x => x.TeamListId == teamList.Id).Include(x => x.Team).ToList();
            var Teams = new Dictionary<int, Tuple<Team, bool>>();
            int GameId = db.Tournaments.Where(t => t.Id == teamList.TournamentId).Select(t => t.GameId).First();
            var AllTeams = db.Teams.Where(t => t.GameId == GameId).ToDictionary(x => x.Id);
            foreach (var e in AllTeams)
            {
                Teams.Add(e.Key, new Tuple<Team, bool>(e.Value, false));
            }
            foreach (var t in TeamListTeams)
            {
                Teams[t.TeamId] = new Tuple<Team, bool>(t.Team, true);
            }
            return Teams;
        }

        // GET: teamLists/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            TeamList teamList = db.TeamLists.Find(id);
            if (teamList == null)
            {
                return NotFound();
            }
            ViewBag.Teams = GetTeams(teamList);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", teamList.TournamentId);
            return View(teamList);
        }

        // POST: teamLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TeamList teamList, int[] TeamListTeams)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamList).State = EntityState.Modified;
                var ExisitngTeamListTeamsIds = db.TeamListTeams.Where(x => x.TeamListId == teamList.Id).
                    Select(st => st.TeamId).
                    ToDictionary(x => x);
                var NewTeamListTeamsIds = TeamListTeams.ToDictionary(x => x);
                var ToDelete = ExisitngTeamListTeamsIds.Where(x => !NewTeamListTeamsIds.Contains(x)).ToList();
                var ToCreate = NewTeamListTeamsIds.Where(x => !ExisitngTeamListTeamsIds.Contains(x)).ToList();
                foreach (var x in ToDelete)
                {
                    db.Remove(db.TeamListTeams.Where(t => t.TeamListId == teamList.Id && t.TeamId == x.Key).First());
                }
                foreach (var x in ToCreate)
                {
                    var NewTLT = new TeamListTeam
                    {
                        TeamListId = teamList.Id,
                        TeamId = x.Key
                    };
                    db.TeamListTeams.Add(NewTLT);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Teams = GetTeams(teamList);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", teamList.TournamentId);
            return View(teamList);
        }

        // GET: teamLists/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            TeamList teamList = db.TeamLists.Find(id);
            if (teamList == null)
            {
                return NotFound();
            }
            return View(teamList);
        }

        // POST: teamLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TeamList teamList = db.TeamLists.Find(id);
            db.TeamLists.Remove(teamList);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(teamList);
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
