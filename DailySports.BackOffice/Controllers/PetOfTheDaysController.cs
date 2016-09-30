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
    public class PetOfTheDaysController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: PetOfTheDays
        public ActionResult Index()
        {
            return View(db.PetOfTheDay.ToList());
        }

        // GET: PetOfTheDays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PetOfTheDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,PetImage,Age,Gender,FunFact,Owner,Date")] PetOfTheWeek petOfTheDay,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Attachments/Images"), fileName);
                file.SaveAs(path);
                petOfTheDay.PetImage = path;
                db.PetOfTheDay.Add(petOfTheDay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(petOfTheDay);
        }

        // GET: PetOfTheDays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetOfTheWeek petOfTheDay = db.PetOfTheDay.Find(id);
            if (petOfTheDay == null)
            {
                return HttpNotFound();
            }
            return View(petOfTheDay);
        }

        // POST: PetOfTheDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,PetImage,Age,Gender,FunFact,Owner,Date")] PetOfTheWeek petOfTheDay,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Attachments/Images"), fileName);
                file.SaveAs(path);
                petOfTheDay.PetImage = path;
                db.Entry(petOfTheDay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(petOfTheDay);
        }

        // GET: PetOfTheDays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetOfTheWeek petOfTheDay = db.PetOfTheDay.Find(id);
            if (petOfTheDay == null)
            {
                return HttpNotFound();
            }
            return View(petOfTheDay);
        }

        // POST: PetOfTheDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PetOfTheWeek petOfTheDay = db.PetOfTheDay.Find(id);
            db.PetOfTheDay.Remove(petOfTheDay);
            db.SaveChanges();
            return RedirectToAction("Index");
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
