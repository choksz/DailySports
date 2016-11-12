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
    public class NewsController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());



        // GET: News
        public IActionResult Index()
        {
            var news = db.News.Include(n => n.Author).Include(n => n.category).Include(n => n.game).Include(n => n.Tournament);
            return View(news.ToList());
        }

        // GET: News/Create
        public IActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name");
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name");
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View(new News());
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description,NewsImage,Date,Tag,AuthorId,CategoryId,GameId,status,TournamentId")] News news,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    news.NewsImage = GoogleStorageService.Upload(file);
                }
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "Name", news.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", news.CategoryId);
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", news.GameId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", news.TournamentId);
            return View(news);
        }

        // GET: News/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "Name", news.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", news.CategoryId);
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", news.GameId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", news.TournamentId);
            ViewBag.oldFileName = news.NewsImage;
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Title,Description,NewsImage,Date,Tag,AuthorId,CategoryId,GameId,status,TournamentId")] News news,IFormFile file, string oldFileName)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (oldFileName.Length > 0)
                    {
                        GoogleStorageService.Delete(oldFileName);
                    }
                    news.NewsImage = GoogleStorageService.Upload(file);
                }
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "Name", news.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", news.CategoryId);
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", news.GameId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", news.TournamentId);
            return View(news);
        }

        // GET: News/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            GoogleStorageService.Delete(news.NewsImage);
            db.News.Remove(news);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(news);
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
            return Redirect("https://localhost:44319/News/Index");
        }
    }
}
