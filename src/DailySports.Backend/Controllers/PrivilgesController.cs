using DailySports.DataLayer.Context;
using DailySports.DataLayer.Model;
using DailySports.ServiceLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DailySports.Backend.Controllers
{
    public class PrivilgesController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());



        // GET: Privilges
        public IActionResult Index()
        {
            return View(db.Privilge.ToList());
        }
        // GET: Privilges/Create
        public IActionResult Create()
        {
            return View(new Privilge());
        }

        // POST: Privilges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Privilge privilge)
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
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Privilge privilge = db.Privilge.Find(id);
            if (privilge == null)
            {
                return NotFound();
            }
            return View(privilge);
        }

        // POST: Privilges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Name")] Privilge privilge)
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Privilge privilge = db.Privilge.Find(id);
            if (privilge == null)
            {
                return NotFound();
            }
            return View(privilge);
        }

        // POST: Privilges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Privilge privilge = db.Privilge.Find(id);
            db.Privilge.Remove(privilge);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
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
