using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DailySports.DataLayer.Context;
using DailySports.DataLayer.Model;
using System.IO;
using DailySports.BackOffice.Utilities;

namespace DailySports.BackOffice.Controllers
{
    public class EventImagesController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: EventImages
        public ActionResult Index()
        {
            var eventImages = db.EventImages.Include(e => e.Event);
            return View(eventImages.ToList());
        }

        // GET: EventImages/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title");
            return View();
        }

        // POST: EventImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tag,File,EventId")] EventImage eventImage, HttpPostedFileBase file)
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventImage eventImage = db.EventImages.Find(id);
            if (eventImage == null)
            {
                return HttpNotFound();
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
        public ActionResult Edit([Bind(Include = "Id,Tag,File,EventId")] EventImage eventImage, HttpPostedFileBase file, string oldFileName)
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventImage eventImage = db.EventImages.Find(id);
            if (eventImage == null)
            {
                return HttpNotFound();
            }
            return View(eventImage);
        }

        // POST: EventImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventImage eventImage = db.EventImages.Find(id);
            GoogleStorageService.Delete(eventImage.File);
            db.EventImages.Remove(eventImage);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
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
        public ActionResult Preview(int id)
        {
            return Redirect("https://localhost:44319/Event/GetEvent/id");
        }
    }
}
