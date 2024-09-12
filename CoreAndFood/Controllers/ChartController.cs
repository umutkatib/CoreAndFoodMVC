using CoreAndFood.Data;
using CoreAndFood.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;

namespace CoreAndFood.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult VisualizeProductResult()
        {
            return Json(ProList());
        }

        public List<Class1> ProList()
        {
            List<Class1> cs = new List<Class1>();
            cs.Add(new Class1()
            {
                ProName = "Computer",
                Stock = 145
            });
            cs.Add(new Class1()
            {
                ProName = "UHD",
                Stock = 100
            });
            return cs;
        }

        public IActionResult Index3()
        {
            return View();
        }

        public IActionResult VisualizeProductResult3()
        {
            return Json(FoodList());
        }
        
        public List<Class2> FoodList()
        {
            List<Class2> cs3 = new List<Class2>();
            using(var c = new Context())
            {
                cs3 = c.Foods.Select(x => new Class2
                {
                    foodname = x.Name,
                    stock = x.Stock,
                }).ToList();
            }
            return cs3;
        }

        public IActionResult Statistics()
        {
            Context context = new Context();

            var value1 = context.Foods.Count();
            ViewBag.v1 = value1;

            var value2 = context.Categories.Where(x => x.Status == true).Count();
            ViewBag.v2 = value2;

            var value3 = context.Foods.Sum(x => x.Stock);
            ViewBag.v3 = value3;

            var fooid = context.Categories.Where(x => x.CategoryName == "Fruit").Select(y => y.CategoryID).FirstOrDefault();
            var value4 = context.Foods.Where(x => x.CategoryID == fooid).Count();
            ViewBag.v4 = value4;

            var value5 = context.Foods.Where(x => x.CategoryID == (context.Categories.Where(z => z.CategoryName == "Vegetables").Select(v => v.CategoryID).FirstOrDefault())).Count();
            ViewBag.v5 = value5;

            var value6 = context.Foods.Where(x => x.CategoryID == (context.Categories.Where(z => z.CategoryName == "Legumes").Select(v => v.CategoryID).FirstOrDefault())).Count();
            ViewBag.v6 = value6;

            var value7 = context.Foods.OrderByDescending(x => x.Stock).Select(y => y.Name).FirstOrDefault();
            ViewBag.v7 = value7;

            var value8 = context.Foods.OrderBy(x => x.Stock).Select(y => y.Name).FirstOrDefault();
            ViewBag.v8 = value8;

            var value9 = context.Foods.Average(x => x.Price);
            ViewBag.v9 = value9;

            var value10 = context.Categories.Where(x => x.CategoryName == "Fruit").Select(y => y.CategoryID).FirstOrDefault();
            var value10p = context.Foods.Where(x => x.CategoryID == value10).Sum(x => x.Price);
            ViewBag.v10 = value10p;

            var value11 = context.Categories.Where(x => x.CategoryName == "Vegetables").Select(y => y.CategoryID).FirstOrDefault();
            var value11p = context.Foods.Where(x => x.CategoryID == value11).Sum(x => x.Price);
            ViewBag.v11 = value11p;

            var value12 = context.Foods.OrderByDescending(y => y.Price).Select(y => y.Name).FirstOrDefault();
            ViewBag.v12 = value12;

            return View();
        }
    }
}
