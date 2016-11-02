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

namespace DailySports.BackOffice.Controllers
{
    public class EventCommentsController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: EventComments
        public ActionResult Index()
        {
            var eventComments = db.EventComments.Include(e => e.Comments).Include(e => e.Event);
            return View(eventComments.ToList());
        }

        // GET: EventComments/Create
        public ActionResult Create()
        {
            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Description");
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title");
            return View();
        }

        // POST: EventComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CommentId,EventId")] EventComments eventComments)
        {
            if (ModelState.IsValid)
            {
                db.EventComments.Add(eventComments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Description", eventComments.CommentId);
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", eventComments.EventId);
            return View(eventComments);
        }

        // GET: EventComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventComments eventComments = db.EventComments.Find(id);
            if (eventComments == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Description", eventComments.CommentId);
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", eventComments.EventId);
            return View(eventComments);
        }

        // POST: EventComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CommentId,EventId")] EventComments eventComments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventComments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Description", eventComments.CommentId);
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", eventComments.EventId);
            return View(eventComments);
        }

        // GET: EventComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventComments eventComments = db.EventComments.Find(id);
            if (eventComments == null)
            {
                return HttpNotFound();
            }
            return View(eventComments);
        }

        // POST: EventComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventComments eventComments = db.EventComments.Find(id);
            db.EventComments.Remove(eventComments);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(eventComments);
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
