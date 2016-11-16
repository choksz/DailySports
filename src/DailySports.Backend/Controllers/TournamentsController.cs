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
    public class TournamentsController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());



        // GET: Tournaments
        public IActionResult Index()
        {
            var tournaments = db.Tournaments.Include(t => t.Game);
            return View(tournaments.ToList());
        }

        // GET: Tournaments/Create
        public IActionResult Create()
        {
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name");
            return View(new Tournaments());
        }

        // POST: Tournaments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tournaments tournaments, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    tournaments.TournamentImage = GoogleStorageService.Upload(file);
                }
                db.Tournaments.Add(tournaments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", tournaments.GameId);
            return View(tournaments);
        }

        // GET: Tournaments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Tournaments tournaments = db.Tournaments.Find(id);
            if (tournaments == null)
            {
                return NotFound();
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", tournaments.GameId);
            ViewBag.oldFileName = tournaments.TournamentImage;
            return View(tournaments);
        }

        // POST: Tournaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tournaments tournaments, IFormFile file, string oldFileName)
        {
            tournaments.TournamentImage = oldFileName;
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (oldFileName != null && oldFileName.Length > 0)
                    {
                        GoogleStorageService.Delete(oldFileName);
                    }
                    tournaments.TournamentImage = GoogleStorageService.Upload(file);
                }
                db.Entry(tournaments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.oldFileName = tournaments.TournamentImage;
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", tournaments.GameId);
            return View(tournaments);
        }

        // GET: Tournaments/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Tournaments tournaments = db.Tournaments.Find(id);
            if (tournaments == null)
            {
                return NotFound();
            }
            return View(tournaments);
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Tournaments tournaments = db.Tournaments.Find(id);
            GoogleStorageService.Delete(tournaments.TournamentImage);
            db.Tournaments.Remove(tournaments);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(tournaments);
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
        public IActionResult Preview()
        {
            return Redirect("https://localhost:44319/Tournament/Index");
        }
    }
}
