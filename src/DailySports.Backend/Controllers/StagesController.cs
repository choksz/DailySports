﻿using DailySports.DataLayer.Context;
using DailySports.DataLayer.Model;
using DailySports.ServiceLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace DailySports.Backend.Controllers
{
    public class StagesController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());

        // GET: stages
        public IActionResult Index()
        {
            var stages = db.Stages.Include(g => g.Tournament);
            return View(stages.ToList());
        }

        // GET: stages/Create
        public IActionResult Create()
        {
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View(new Stage());
        }

        // POST: stages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Stage stage, int[] StageTeams)
        {
            if (ModelState.IsValid)
            {
                db.Stages.Add(stage);
                int id = db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", stage.TournamentId);
            return View(stage);
        }

        // GET: stages/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Stage stage = db.Stages.Find(id);
            if (stage == null)
            {
                return NotFound();
            }
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", stage.TournamentId);
            return View(stage);
        }

        // POST: stages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Stage stage, int[] StageTeams)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stage).State = EntityState.Modified;
                db.SaveChanges(); 
                return RedirectToAction("Index");
            }
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", stage.TournamentId);
            return View(stage);
        }

        // GET: stages/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Stage stage = db.Stages.Find(id);
            if (stage == null)
            {
                return NotFound();
            }
            return View(stage);
        }

        // POST: stages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Stage stage = db.Stages.Find(id);
            db.Stages.Remove(stage);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(stage);
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
