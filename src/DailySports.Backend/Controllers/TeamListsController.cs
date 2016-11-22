using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DailySports.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using DailySports.DataLayer.Model;
using DailySports.ServiceLayer.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DailySports.Backend.Controllers
{
    public class TeamListsController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());

        // GET: TeamList
        public ActionResult Index()
        {
            return View(db.TeamLists.ToList());
        }

        // GET: TeamList/Create
        public ActionResult Create()
        {
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View(new TeamList());
        }

        // POST: TeamList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamList entry)
        {
            if (ModelState.IsValid)
            {
                db.TeamLists.Add(entry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View(entry);
        }

        // GET: TeamList/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            TeamList entry = db.TeamLists.Find(id);
            if (entry == null)
            {
                return NotFound();
            }
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View(entry);
        }

        // POST: TeamList/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamList entry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View(entry);
        }

        // GET: TeamList/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            TeamList entry = db.TeamLists.Find(id);
            if (entry == null)
            {
                return NotFound();
            }
            return View(entry);
        }

        // POST: TeamList/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            TeamList entry = db.TeamLists.Find(id);
            try
            {
                db.TeamLists.Remove(entry);
                db.SaveChanges();
            }
            catch (Exception)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(entry);
            }
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