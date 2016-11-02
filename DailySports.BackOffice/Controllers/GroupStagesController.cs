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
    public class GroupStagesController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: GroupStages
        public ActionResult Index()
        {
            var groupStages = db.GroupStages.Include(g => g.Tournament);
            return View(groupStages.ToList());
        }

        // GET: GroupStages/Create
        public ActionResult Create()
        {
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title");
            return View();
        }

        // POST: GroupStages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TournamentId")] GroupStages groupStages)
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupStages groupStages = db.GroupStages.Find(id);
            if (groupStages == null)
            {
                return HttpNotFound();
            }
            ViewBag.TournamentId = new SelectList(db.Tournaments, "Id", "Title", groupStages.TournamentId);
            return View(groupStages);
        }

        // POST: GroupStages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TournamentId")] GroupStages groupStages)
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupStages groupStages = db.GroupStages.Find(id);
            if (groupStages == null)
            {
                return HttpNotFound();
            }
            return View(groupStages);
        }

        // POST: GroupStages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupStages groupStages = db.GroupStages.Find(id);
            db.GroupStages.Remove(groupStages);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
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
