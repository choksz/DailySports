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
    public class PrizePoolsController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());

        // GET: PrizePools
        public IActionResult Index()
        {
            var prizePools = db.PrizePools.Include(p => p.Tournament);
            return View(prizePools.ToList());
        }

        // GET: PrizePools/Create
        public IActionResult Create()
        {
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View(new PrizePool());
        }

        // POST: PrizePools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PrizePool prizePool)
        {
            if (ModelState.IsValid)
            {
                db.PrizePools.Add(prizePool);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", prizePool.TournamentId);
            return View(prizePool);
        }

        // GET: PrizePools/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            PrizePool prizePool = db.PrizePools.Find(id);
            if (prizePool == null)
            {
                return NotFound();
            }
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", prizePool.TournamentId);
            return View(prizePool);
        }

        // POST: PrizePools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PrizePool prizePool)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prizePool).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", prizePool.TournamentId);
            return View(prizePool);
        }

        // GET: PrizePools/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            PrizePool prizePool = db.PrizePools.Find(id);
            if (prizePool == null)
            {
                return NotFound();
            }
            return View(prizePool);
        }

        // POST: PrizePools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            PrizePool prizePool = db.PrizePools.Find(id);
            db.PrizePools.Remove(prizePool);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(prizePool);
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
