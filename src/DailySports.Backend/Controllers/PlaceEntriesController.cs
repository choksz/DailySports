using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DailySports.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using DailySports.DataLayer.Model;
using DailySports.ServiceLayer.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DailySports.Backend.Controllers
{
    public class PlaceEntriesController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());
        
        // GET: PlaceEntry
        public ActionResult Index()
        {
            var placeEntries = db.PlaceEntries.
                Include(e => e.PrizePool).
                    ThenInclude(p => p.Tournament).
                Include(e => e.Team).
                ToList();
            return View(placeEntries);
        }

        private SelectList GetPrizePoolSelectList()
        {
            var list = db.PrizePools.
                Join(db.Tournaments, p => p.TournamentId, t => t.Id, (p, t) => new { prizePool = p, tour = t }).
                Select(x => new { x.prizePool.Id, x.tour.Title }).ToList();
            var items = new List<SelectListItem>();
            foreach (var x in list)
            {
                items.Add(new SelectListItem
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                });
            }
            return new SelectList(items, "Value", "Text");
        }

        // GET: PlaceEntry/Create
        public ActionResult Create()
        {

            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.PrizePoolId = GetPrizePoolSelectList();
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
            ViewBag.PrizePoolId = GetPrizePoolSelectList();
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
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", entry.TeamId);
            ViewBag.PrizePoolId = new SelectList(GetPrizePoolSelectList(), entry.PrizePoolId);
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
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", entry.TeamId);
            ViewBag.PrizePoolId = new SelectList(GetPrizePoolSelectList(), entry.PrizePoolId);
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