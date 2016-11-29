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
    public class EventsController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());



        // GET: Events
        public IActionResult Index()
        {
            var events = db.Events.
                Include(e => e.ticket).
                Include(e => e.Game);
            return View(events.ToList());
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewBag.ticketid = new SelectList(db.Tickets, "Id", "Notes");
            ViewBag.gameid = new SelectList(db.Games, "Id", "Name");
            return View(new Event());
        }
        
        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event @event,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    @event.EventImage = GoogleStorageService.Upload(file);
                }
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ticketid = new SelectList(db.Tickets, "Id", "Notes", @event.ticketid);
            ViewBag.gameid = new SelectList(db.Games, "Id", "Name", @event.GameId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewBag.ticketid = new SelectList(db.Tickets, "Id", "Notes", @event.ticketid);
            ViewBag.gameid = new SelectList(db.Games, "Id", "Name", @event.GameId);
            ViewBag.oldFileName = @event.EventImage;
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Event @event,IFormFile file, string oldFileName)
        {
            @event.EventImage = oldFileName;
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (oldFileName != null && oldFileName.Length > 0)
                    {
                        GoogleStorageService.Delete(oldFileName);
                    }
                    @event.EventImage = GoogleStorageService.Upload(file);
                }
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.oldFileName = @event.EventImage;
            ViewBag.ticketid = new SelectList(db.Tickets, "Id", "Notes", @event.ticketid);
            ViewBag.gameid = new SelectList(db.Games, "Id", "Name", @event.GameId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            GoogleStorageService.Delete(@event.EventImage);
            db.Events.Remove(@event);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(@event);
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
            return Redirect("https://localhost:44319/Event/Index");
        }
    }
}
