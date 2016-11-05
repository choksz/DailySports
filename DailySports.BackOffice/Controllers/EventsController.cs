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
    public class EventsController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: Events
        public ActionResult Index()
        {
            var events = db.Events.Include(e =>e.ticket);
     
            return View(events.ToList());
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.ticketid = new SelectList(db.Tickets, "Id", "Notes");
            return View();
        }
        public string GetBaseUrl()
        {
            var request = System.Web.HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }
        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Tag,Description,Location,Country,Region,City,EventImage,StartDate,EndDate,Currency,Price,ticketid")] Event @event,HttpPostedFileBase file)
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
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.ticketid = new SelectList(db.Tickets, "Id", "Notes", @event.ticketid);
            ViewBag.oldFileName = @event.EventImage;
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Tag,Description,Location,Country,Region,City,EventImage,StartDate,EndDate,Currency,Price,ticketid")] Event @event,HttpPostedFileBase file, string oldFileName)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (oldFileName.Length > 0)
                    {
                        GoogleStorageService.Delete(oldFileName);
                    }
                    @event.EventImage = GoogleStorageService.Upload(file);
                }
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ticketid = new SelectList(db.Tickets, "Id", "Notes", @event.ticketid);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            GoogleStorageService.Delete(@event.EventImage);
            db.Events.Remove(@event);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
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
        public ActionResult Preview()
        {
            return Redirect("https://localhost:44319/Event/Index");
        }
    }
}
