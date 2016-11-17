using DailySports.Backend.Utilities;
using DailySports.DataLayer.Context;
using DailySports.DataLayer.Model;
using DailySports.ServiceLayer.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DailySports.Backend.Controllers
{
    public class CarouselItemsController : Controller
    {
        private DailySportsContext db = new DailySportsContext(new DbContextOptions<DailySportsContext>());

        // GET: CarouselItems
        public IActionResult Index()
        {
            var carouselItems = db.CarouselItems;
            return View(carouselItems.ToList());
        }

        // GET: CarouselItems/Create
        public IActionResult Create()
        {
            return View(new CarouselItem());
        }

        // POST: CarouselItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarouselItem item, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    item.Image = GoogleStorageService.Upload(file);
                }
                db.CarouselItems.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: CarouselItems/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CarouselItem carouselItem = db.CarouselItems.Find(id);
            if (carouselItem == null)
            {
                return NotFound();
            }
            ViewBag.oldFileName = carouselItem.Image;
            return View(carouselItem);
        }

        // POST: CarouselItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarouselItem item, IFormFile file, string oldFileName)
        {
            item.Image = oldFileName;
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (oldFileName != null &&  oldFileName.Length > 0)
                    {
                        GoogleStorageService.Delete(oldFileName);
                    }
                    item.Image = GoogleStorageService.Upload(file);
                }
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.oldFileName = item.Image;
            return View(item);
        }

        // GET: CarouselItems/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CarouselItem item = db.CarouselItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: CarouselItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            CarouselItem item = db.CarouselItems.Find(id);
            GoogleStorageService.Delete(item.Image);
            db.CarouselItems.Remove(item);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            { //there may be foreign key to this object
                ModelState.AddModelError("", "Can't delete this object. Check if other objects don't have foreign key to this.");
                return View(item);
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

        public IActionResult Preview(int id)
        {
            return Redirect("https://localhost:44319/Event/GetEvent/id");
        }
    }
}
