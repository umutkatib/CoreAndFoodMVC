using CoreAndFood.Data.Models;
using CoreAndFood.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using X.PagedList;

namespace CoreAndFood.Controllers
{
    public class FoodController : Controller
    {
        Context c = new Context();
        FoodRepository foodRepository = new FoodRepository();
        public IActionResult Index(int page = 1)
        {
            return View(foodRepository.TList("Category").ToPagedList(page, 3));
        }

        [HttpGet]
        public IActionResult FoodAdd()
        {
            List<SelectListItem> categories = (from x in c.Categories.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryID.ToString()
                                               }).ToList();
            ViewBag.categories = categories;
            return View();
        }
        [HttpPost]
        public IActionResult FoodAdd(FoodAdd p)
        {
            Food f = new Food();
            if(p.ImageURL != null)
            {
                var extension = Path.GetExtension(p.ImageURL.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/" + newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.ImageURL.CopyTo(stream);
                f.ImageURL = "images/" + newimagename;
            }
            f.Name = p.Name;
            f.Description = p.Description;
            f.CategoryID = p.CategoryID;
            f.Price = p.Price;
            f.Stock = p.Stock;

            foodRepository.TAdd(f);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FoodGet(int id)
        {
            List<SelectListItem> categories = (from y in c.Categories.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = y.CategoryName,
                                                   Value = y.CategoryID.ToString()
                                               }).ToList();
            ViewBag.categories = categories;
            var x = foodRepository.TGet(id);
            Food fd = new Food
            {
                CategoryID = x.CategoryID,
                FoodID = x.FoodID,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price, 
                Stock = x.Stock,
                ImageURL = x.ImageURL
            };
            return View(fd);
        }

        [HttpPost]
        public IActionResult FoodUpdate(Food f)
        {
            var fd = foodRepository.TGet(f.FoodID);
            fd.Name = f.Name;
            fd.Stock = f.Stock;
            fd.Price = f.Price;
            fd.ImageURL = f.ImageURL;
            fd.Description = f.Description; 
            fd.CategoryID = f.CategoryID;
            foodRepository.TUpdate(fd);
            return RedirectToAction("Index");
        }

        public IActionResult FoodDelete(int id)
        {
            foodRepository.TRemove(new Food { FoodID = id });
            return RedirectToAction("Index");
        }
    }
}
