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
    public class PetOfTheDaysController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());



        // GET: PetOfTheWeeks
        public IActionResult Index()
        {
            return View(db.PetOfTheWeek.ToList());
        }

        // GET: PetOfTheWeeks/Create
        public IActionResult Create()
        {
            return View(new PetOfTheWeek());
        }

        // POST: PetOfTheWeeks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description,PetImage,Age,Gender,FunFact,Owner,StartDate,EndDate")] PetOfTheWeek petOfTheWeek, IFormFile file)
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
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            PetOfTheWeek petOfTheWeek = db.PetOfTheWeek.Find(id);
            if (petOfTheWeek == null)
            {
                return NotFound();
            }
            ViewBag.oldFileName = petOfTheWeek.PetImage;
            return View(petOfTheWeek);
        }

        // POST: PetOfTheWeek/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Title,Description,PetImage,Age,Gender,FunFact,Owner,StartDate,EndDate")] PetOfTheWeek petOfTheWeek, IFormFile file, string oldFileName)
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            PetOfTheWeek petOfTheWeek = db.PetOfTheWeek.Find(id);
            if (petOfTheWeek == null)
            {
                return NotFound();
            }
            return View(petOfTheWeek);
        }

        // POST: PetOfTheWeek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            PetOfTheWeek petOfTheWeek = db.PetOfTheWeek.Find(id);
            GoogleStorageService.Delete(petOfTheWeek.PetImage);
            db.PetOfTheWeek.Remove(petOfTheWeek);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
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
