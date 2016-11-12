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
    public class GroupStagesController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());



        // GET: GroupStages
        public IActionResult Index()
        {
            var groupStages = db.GroupStages.Include(g => g.Tournament);
            return View(groupStages.ToList());
        }

        // GET: GroupStages/Create
        public IActionResult Create()
        {
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View(new GroupStages());
        }

        // POST: GroupStages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,TournamentId")] GroupStages groupStages)
        {
            if (ModelState.IsValid)
            {
                db.GroupStages.Add(groupStages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", groupStages.TournamentId);
            return View(groupStages);
        }

        // GET: GroupStages/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            GroupStages groupStages = db.GroupStages.Find(id);
            if (groupStages == null)
            {
                return NotFound();
            }
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", groupStages.TournamentId);
            return View(groupStages);
        }

        // POST: GroupStages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,TournamentId")] GroupStages groupStages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupStages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", groupStages.TournamentId);
            return View(groupStages);
        }

        // GET: GroupStages/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            GroupStages groupStages = db.GroupStages.Find(id);
            if (groupStages == null)
            {
                return NotFound();
            }
            return View(groupStages);
        }

        // POST: GroupStages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            GroupStages groupStages = db.GroupStages.Find(id);
            db.GroupStages.Remove(groupStages);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(groupStages);
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
