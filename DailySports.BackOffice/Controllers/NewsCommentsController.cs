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
    public class NewsCommentsController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: NewsComments
        public ActionResult Index()
        {
            var newsComments = db.NewsComments.Include(n => n.Comment).Include(n => n.News);
            return View(newsComments.ToList());
        }

        // GET: NewsComments/Create
        public ActionResult Create()
        {
            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Description");
            ViewBag.NewsId = new SelectList(db.News, "Id", "Title");
            return View();
        }


        // POST: NewsComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CommentId,NewsId")] NewsComments newsComments)
        {
            if (ModelState.IsValid)
            {

                db.NewsComments.Add(newsComments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Description", newsComments.CommentId);
            ViewBag.NewsId = new SelectList(db.News, "Id", "Title", newsComments.NewsId);
            return View(newsComments);
        }

        // GET: NewsComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsComments newsComments = db.NewsComments.Find(id);
            if (newsComments == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Description", newsComments.CommentId);
            ViewBag.NewsId = new SelectList(db.News, "Id", "Title", newsComments.NewsId);
            return View(newsComments);
        }

        // POST: NewsComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CommentId,NewsId")] NewsComments newsComments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsComments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Description", newsComments.CommentId);
            ViewBag.NewsId = new SelectList(db.News, "Id", "Title", newsComments.NewsId);
            return View(newsComments);
        }

        // GET: NewsComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsComments newsComments = db.NewsComments.Find(id);
            if (newsComments == null)
            {
                return HttpNotFound();
            }
            return View(newsComments);
        }

        // POST: NewsComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsComments newsComments = db.NewsComments.Find(id);
            db.NewsComments.Remove(newsComments);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(newsComments);
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
