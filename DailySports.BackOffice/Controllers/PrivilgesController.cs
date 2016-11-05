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
    public class PrivilgesController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: Privilges
        public ActionResult Index()
        {
            return View(db.Privilge.ToList());
        }
        // GET: Privilges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Privilges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Privilge privilge)
        {
            if (ModelState.IsValid)
            {
                db.Privilge.Add(privilge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(privilge);
        }

        // GET: Privilges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Privilge privilge = db.Privilge.Find(id);
            if (privilge == null)
            {
                return HttpNotFound();
            }
            return View(privilge);
        }

        // POST: Privilges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Privilge privilge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(privilge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(privilge);
        }

        // GET: Privilges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Privilge privilge = db.Privilge.Find(id);
            if (privilge == null)
            {
                return HttpNotFound();
            }
            return View(privilge);
        }

        // POST: Privilges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Privilge privilge = db.Privilge.Find(id);
            db.Privilge.Remove(privilge);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(privilge);
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
