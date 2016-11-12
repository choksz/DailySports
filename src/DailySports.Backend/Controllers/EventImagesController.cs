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
    public class EventImagesController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());



        // GET: EventImages
        public IActionResult Index()
        {
            var eventImages = db.EventImages.Include(e => e.Event);
            return View(eventImages.ToList());
        }

        // GET: EventImages/Create
        public IActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title");
            return View(new EventImage());
        }

        // POST: EventImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Tag,File,EventId")] EventImage eventImage, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    eventImage.File = GoogleStorageService.Upload(file);
                }
                db.EventImages.Add(eventImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", eventImage.EventId);
            return View(eventImage);
        }

        // GET: EventImages/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            EventImage eventImage = db.EventImages.Find(id);
            if (eventImage == null)
            {
                return NotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", eventImage.EventId);
            ViewBag.oldFileName = eventImage.File;
            return View(eventImage);
        }

        // POST: EventImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Tag,File,EventId")] EventImage eventImage, IFormFile file, string oldFileName)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (oldFileName.Length > 0)
                    {
                        GoogleStorageService.Delete(oldFileName);
                    }
                    eventImage.File = GoogleStorageService.Upload(file);
                }
                db.Entry(eventImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", eventImage.EventId);
            return View(eventImage);
        }

        // GET: EventImages/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            EventImage eventImage = db.EventImages.Find(id);
            if (eventImage == null)
            {
                return NotFound();
            }
            return View(eventImage);
        }

        // POST: EventImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            EventImage eventImage = db.EventImages.Find(id);
            GoogleStorageService.Delete(eventImage.File);
            db.EventImages.Remove(eventImage);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(eventImage);
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
        public IActionResult Preview(int id)
        {
            return Redirect("https://localhost:44319/Event/GetEvent/id");
        }
    }
}
