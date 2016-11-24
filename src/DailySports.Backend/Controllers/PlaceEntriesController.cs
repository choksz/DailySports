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
    public class PlaceEntriesController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());
        
        // GET: PlaceEntry
        public ActionResult Index()
        {
            return View(db.PlaceEntries.Include(e => e.PrizePool).ThenInclude(p => p.Tournament).ToList());
        }

        // GET: PlaceEntry/Create
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.PrizePoolId = new SelectList(db.PrizePools, "Id", "Id");
            return View(new PlaceEntry());
        }

        // POST: PlaceEntry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlaceEntry entry)
        {
            if (ModelState.IsValid)
            {
                db.PlaceEntries.Add(entry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.PrizePoolId = new SelectList(db.PrizePools, "Id", "Id");
            return View(entry);
        }

        // GET: PlaceEntry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            PlaceEntry entry = db.PlaceEntries.Find(id);
            if (entry == null)
            {
                return NotFound();
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.PrizePoolId = new SelectList(db.PrizePools, "Id", "Id");
            return View(entry);
        }

        // POST: PlaceEntry/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlaceEntry entry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.PrizePoolId = new SelectList(db.PrizePools, "Id", "Id");
            return View(entry);
        }

        // GET: PlaceEntry/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            PlaceEntry entry = db.PlaceEntries.Find(id);
            if (entry == null)
            {
                return NotFound();
            }
            return View(entry);
        }

        // POST: PlaceEntry/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            PlaceEntry entry = db.PlaceEntries.Find(id);
            try
            {
                db.PlaceEntries.Remove(entry);
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