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
    public class VideoCommentsController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: VideoComments
        public ActionResult Index()
        {
            var videosComments = db.VideosComments.Include(v => v.Comment).Include(v => v.Video);
            return View(videosComments.ToList());
        }

        // GET: VideoComments/Create
        public ActionResult Create()
        {
            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Description");
            ViewBag.VideoId = new SelectList(db.Videos, "Id", "Title");
            return View();
        }

        // POST: VideoComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CommentId,VideoId")] VideoComments videoComments)
        {
            if (ModelState.IsValid)
            {
                db.VideosComments.Add(videoComments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Description", videoComments.CommentId);
            ViewBag.VideoId = new SelectList(db.Videos, "Id", "Title", videoComments.VideoId);
            return View(videoComments);
        }

        // GET: VideoComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoComments videoComments = db.VideosComments.Find(id);
            if (videoComments == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Description", videoComments.CommentId);
            ViewBag.VideoId = new SelectList(db.Videos, "Id", "Title", videoComments.VideoId);
            return View(videoComments);
        }

        // POST: VideoComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CommentId,VideoId")] VideoComments videoComments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videoComments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Description", videoComments.CommentId);
            ViewBag.VideoId = new SelectList(db.Videos, "Id", "Title", videoComments.VideoId);
            return View(videoComments);
        }

        // GET: VideoComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoComments videoComments = db.VideosComments.Find(id);
            if (videoComments == null)
            {
                return HttpNotFound();
            }
            return View(videoComments);
        }

        // POST: VideoComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VideoComments videoComments = db.VideosComments.Find(id);
            db.VideosComments.Remove(videoComments);
            db.SaveChanges();
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
