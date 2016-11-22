using DailySports.Backend.Utilities;
using DailySports.DataLayer.Context;
using DailySports.DataLayer.Model;
using DailySports.ServiceLayer.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DailySports.Backend.Controllers
{
    public class TeamsController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());

        // GET: Teams
        public IActionResult Index()
        {
            var teams = db.Teams.Include(t => t.Game).Include(t => t.Country);
            return View(teams.ToList());
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name");
            ViewBag.CountryCode = new SelectList(db.Countries, "Code", "Code");
            return View(new Team());
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Team team, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    team.Logo = GoogleStorageService.Upload(file);
                }
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameId = new SelectList(db.Games, "Id", "Name");
            ViewBag.CountryCode = new SelectList(db.Countries, "Code", "Code");
            return View(team);
        }

        // GET: Teams/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name");
            ViewBag.CountryCode = new SelectList(db.Countries, "Code", "Code");
            ViewBag.oldFileName = team.Logo;
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Team team, IFormFile file, string oldFileName)
        {
            team.Logo = oldFileName;
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (oldFileName != null && oldFileName.Length > 0)
                    {
                        GoogleStorageService.Delete(oldFileName);
                    }
                    team.Logo = GoogleStorageService.Upload(file);
                }
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name");
            ViewBag.CountryCode = new SelectList(db.Countries, "Code", "Code");
            ViewBag.oldFileName = team.Logo;
            return View(team);
        }

        // GET: Teams/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            GoogleStorageService.Delete(team.Logo);
            db.Teams.Remove(team);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(team);
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
