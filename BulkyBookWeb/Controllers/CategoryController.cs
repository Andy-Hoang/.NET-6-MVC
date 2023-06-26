﻿using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        //inject dependency in the constructor
        // AppDbContext already added in the Container. so the object "db" with all implementation ready to use.
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> CategoryList = _db.Categories;
            return View(CategoryList);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //key 'name' will bind to show error inside 'Name' prop of Category
                ModelState.TryAddModelError("name", "DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)     //server side validation
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();      //actual action made to Database
                TempData["Success"] = "Category created successfully"; 
                return RedirectToAction("Index");       // if no Controller name, then default is the current Controller
            }
            return View(obj);       //pass the current 'obj' to see the current input values causing error (if in case of error)
            
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDB = _db.Categories.Find(id);
            if(categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //key 'name' will bind to show error inside 'Name' prop of Category
                ModelState.TryAddModelError("name", "DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);     //Entity Framework does all the work: find the 'obj' by id in DB, look for what props changed, and update
                _db.SaveChanges();
                TempData["Success"] = "Category edited successfully";
                return RedirectToAction("Index");       
            }
            return View(obj);       

        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDB = _db.Categories.Find(id);
            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Can pass here both Category obj or id
        // If pass obj: will now to trigger Delete( obj ) for POST instead of Delete (int) for GET
        // If pass id: func will be the same with Delete(int? id) for Get -> so have to change named to DeletePost()
        public IActionResult DeletePost(int? id)        //can pass here both Category obj or id | Pass id to mak
        {
            var categoryFromDB = _db.Categories.Find(id);
            if (categoryFromDB == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(categoryFromDB);     //Entity Framework does all the work: find the 'obj' by id in DB, look for what props changed, and update
            _db.SaveChanges();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
