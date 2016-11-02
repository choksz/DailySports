using DailySports.DataLayer.Context;
using DailySports.DataLayer.Model;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System;
using DailySports.BackOffice.Utilities;

namespace DailySports.BackOffice.Controllers
{
    public class PetOfTheDaysController : Controller
    {
        private DailySportsContext db = new DailySportsContext();

        // GET: PetOfTheWeeks
        public ActionResult Index()
        {
            return View(db.PetOfTheWeek.ToList());
        }

        // GET: PetOfTheWeeks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PetOfTheWeeks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,PetImage,Age,Gender,FunFact,Owner,StartDate,EndDate")] PetOfTheWeek petOfTheWeek, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    petOfTheWeek.PetImage = GoogleStorageService.Upload(file);
                }
                db.PetOfTheWeek.Add(petOfTheWeek);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(petOfTheWeek);
        }

        // GET: PetOfTheWeek/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetOfTheWeek petOfTheWeek = db.PetOfTheWeek.Find(id);
            if (petOfTheWeek == null)
            {
                return HttpNotFound();
            }
            ViewBag.oldFileName = petOfTheWeek.PetImage;
            return View(petOfTheWeek);
        }

        // POST: PetOfTheWeek/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,PetImage,Age,Gender,FunFact,Owner,StartDate,EndDate")] PetOfTheWeek petOfTheWeek, HttpPostedFileBase file, string oldFileName)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (oldFileName.Length > 0)
                    {
                        GoogleStorageService.Delete(oldFileName);
                    }
                    petOfTheWeek.PetImage = GoogleStorageService.Upload(file);
                }
                db.Entry(petOfTheWeek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(petOfTheWeek);
        }

        // GET: PetOfTheWeek/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetOfTheWeek petOfTheWeek = db.PetOfTheWeek.Find(id);
            if (petOfTheWeek == null)
            {
                return HttpNotFound();
            }
            return View(petOfTheWeek);
        }

        // POST: PetOfTheWeek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PetOfTheWeek petOfTheWeek = db.PetOfTheWeek.Find(id);
            GoogleStorageService.Delete(petOfTheWeek.PetImage);
            db.PetOfTheWeek.Remove(petOfTheWeek);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(petOfTheWeek);
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
