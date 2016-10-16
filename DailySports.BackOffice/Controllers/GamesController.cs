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

namespace DailySports.BackOffice.Controllers
{
    public class GamesController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: Games
        public ActionResult Index()
        {
            return View(db.Games.ToList());
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,GameImage")] Game game,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var fileName = DateTime.Now.ToString("ddMMyyyyhhmmssffff") + "_" + Path.GetFileName(file.FileName);
                var virtualpath = "backend/Attachments/Games/" + fileName;
                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Attachments/Games"), fileName);
                file.SaveAs(path);
                game.GameImage = virtualpath;
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,GameImage")] Game game, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var fileName = DateTime.Now.ToString("ddMMyyyyhhmmssffff") + "_" + Path.GetFileName(file.FileName);
                var virtualpath = "backend/Attachments/Games/" + fileName;
                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Attachments/Games"), fileName);
                file.SaveAs(path);
                game.GameImage=virtualpath;
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            } catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(game);
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
