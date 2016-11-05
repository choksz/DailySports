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
using DailySports.BackOffice.Utilities;
using System.IO;

namespace DailySports.BackOffice.Controllers
{
    public class NewsController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: News
        public ActionResult Index()
        {
            var news = db.News.Include(n => n.Author).Include(n => n.category).Include(n => n.game).Include(n => n.Tournament);
            return View(news.ToList());
        }

        // GET: News/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name");
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name");
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,NewsImage,Date,Tag,AuthorId,CategoryId,GameId,status,TournamentId")] News news,HttpPostedFileBase file)
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
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
        public ActionResult Edit([Bind(Include = "Id,Title,Description,NewsImage,Date,Tag,AuthorId,CategoryId,GameId,status,TournamentId")] News news,HttpPostedFileBase file, string oldFileName)
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            GoogleStorageService.Delete(news.NewsImage);
            db.News.Remove(news);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
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
        public ActionResult Preview()
        {
            return Redirect("https://localhost:44319/News/Index");
        }
    }
}
