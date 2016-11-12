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
    public class VideoCommentsController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());



        // GET: VideoComments
        public IActionResult Index()
        {
            var videosComments = db.VideosComments.Include(v => v.Comment).Include(v => v.Video);
            return View(videosComments.ToList());
        }

        // GET: VideoComments/Create
        public IActionResult Create()
        {
            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Description");
            ViewBag.VideoId = new SelectList(db.Videos, "Id", "Title");
            return View(new VideoComments());
        }

        // POST: VideoComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CommentId,VideoId")] VideoComments videoComments)
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
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            VideoComments videoComments = db.VideosComments.Find(id);
            if (videoComments == null)
            {
                return NotFound();
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
        public IActionResult Edit([Bind("Id,CommentId,VideoId")] VideoComments videoComments)
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            VideoComments videoComments = db.VideosComments.Find(id);
            if (videoComments == null)
            {
                return NotFound();
            }
            return View(videoComments);
        }

        // POST: VideoComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            VideoComments videoComments = db.VideosComments.Find(id);
            db.VideosComments.Remove(videoComments);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(videoComments);
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
