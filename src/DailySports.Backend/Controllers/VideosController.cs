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
    public class VideosController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());



        // GET: Videos
        public IActionResult Index()
        {
            var videos = db.Videos.Include(v => v.Category).Include(v => v.Game).Include(v => v.Tournament);
            return View(videos.ToList());
        }

        // GET: Videos/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name");
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name");
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View(new Videos());
        }

        // POST: Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description,URL,Date,Tag,LiveStreamURL,GameId,CategoryId,TournamentId")] Videos videos)
        {
            if (ModelState.IsValid)
            {
                db.Videos.Add(videos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", videos.CategoryId);
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", videos.GameId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", videos.TournamentId);
            return View(videos);
        }

        // GET: Videos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Videos videos = db.Videos.Find(id);
            if (videos == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", videos.CategoryId);
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", videos.GameId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", videos.TournamentId);
            return View(videos);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Title,Description,URL,Date,Tag,LiveStreamURL,GameId,CategoryId,TournamentId")] Videos videos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", videos.CategoryId);
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", videos.GameId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", videos.TournamentId);
            return View(videos);
        }

        // GET: Videos/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Videos videos = db.Videos.Find(id);
            if (videos == null)
            {
                return NotFound();
            }
            return View(videos);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Videos videos = db.Videos.Find(id);
            db.Videos.Remove(videos);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(videos);
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
            return Redirect("https://localhost:44319/Video/Index");
        }
    }
}
