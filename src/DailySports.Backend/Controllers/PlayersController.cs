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
    public class PlayersController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());



        // GET: Players
        public IActionResult Index()
        {
            var players = db.Players.
                Include(p => p.Team).
                Include(p => p.Country);
            return View(players.ToList());
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.CountryId = new SelectList(db.Countries, "Code", "Name");
            return View(new Player());
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", player.TeamId);
            ViewBag.CountryId = new SelectList(db.Countries, "Code", "Name");
            return View(player);
        }

        // GET: Players/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", player.TeamId);
            ViewBag.CountryId = new SelectList(db.Countries, "Code", "Name");
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", player.TeamId);
            ViewBag.CountryId = new SelectList(db.Countries, "Code", "Name");
            return View(player);
        }

        // GET: Players/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(player);
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
