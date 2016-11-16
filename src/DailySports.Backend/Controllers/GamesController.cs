using DailySports.Backend.Utilities;
using DailySports.DataLayer.Context;
using DailySports.DataLayer.Model;
using DailySports.ServiceLayer.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DailySports.Backend.Controllers
{
    public class GamesController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());



        // GET: Games
        public IActionResult Index()
        {
            return View(db.Games.ToList());
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View(new Game());
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Game game,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    game.GameImage = GoogleStorageService.Upload(file);
                }
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewBag.oldFileName = game.GameImage;
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Game game, IFormFile file, string oldFileName)
        {
            game.GameImage = oldFileName;
            if (ModelState.IsValid)
            { 
                if (file != null)
                {
                    if (oldFileName != null && oldFileName.Length > 0)
                    {
                        GoogleStorageService.Delete(oldFileName);
                    }
                    game.GameImage = GoogleStorageService.Upload(file);
                }
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.oldFileName = game.GameImage;
            return View(game);
        }

        // GET: Games/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            GoogleStorageService.Delete(game.GameImage);
            db.Games.Remove(game);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            } catch (Exception e)
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
