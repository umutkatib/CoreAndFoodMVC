using CoreAndFood.Data.Models;
using CoreAndFood.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreAndFood.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository categoryRepository = new CategoryRepository();

        public IActionResult Index(string p)
        {
            if(!string.IsNullOrEmpty(p))
            {
                return View(categoryRepository.TList(x => x.CategoryName.Contains(p)));
            }
            return View(categoryRepository.TList(x => x.Status == true));
        }

        [HttpGet]
        public IActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryAdd(Category c)
        {
            if(!ModelState.IsValid)
            {
                return View("CategoryAdd");
            }
            c.Status = true;
            categoryRepository.TAdd(c);
            return RedirectToAction("Index");
        }

        public IActionResult CategoryGet(int id)
        {
            var ctgry = categoryRepository.TGet(id);
            Category ct = new Category
            {
                CategoryID = ctgry.CategoryID,
                CategoryName = ctgry.CategoryName,
                CategoryDescription = ctgry.CategoryDescription,
            };

            return View(ct);
        }

        [HttpPost]
        public IActionResult CategoryUpdate(Category c)
        {
            var ctgry = categoryRepository.TGet(c.CategoryID);
            ctgry.CategoryName = c.CategoryName;
            ctgry.CategoryDescription = c.CategoryDescription;
            categoryRepository.TUpdate(ctgry);
            return RedirectToAction("Index");
        }

        public IActionResult CategoryDelete(int id)
        {
            var ctgry = categoryRepository.TGet(id);
            ctgry.Status = false;
            categoryRepository.TUpdate(ctgry);
            return RedirectToAction("Index");
        }
    }
}
