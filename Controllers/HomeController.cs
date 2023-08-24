﻿using Dashboard.Data;
using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		private readonly ApplicationDbContext context;

		public HomeController(ApplicationDbContext context)
		{
			this.context = context;
		}


		public IActionResult Index()
        {
			var product = context.Products.ToList();
			return View(product);
        }

		public IActionResult AddProduct(Product product)
		{
			context.Products.Add(product);
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult CreateNewProduct(Product product)
		{
            context.Products.Add(product);
            context.SaveChanges();
            return RedirectToAction("Index");
		}

        public IActionResult Edit(int id)
        {

            var product = context.Products.SingleOrDefault(p => p.Id == id);

            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProducts(Product product)
        {

            context.Products.Update(product);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
		{

			var product = context.Products.SingleOrDefault(p => p.Id == id);

			if (product != null)
			{
				context.Products.Remove(product);
				context.SaveChanges();
			}
			return RedirectToAction("Index");
		}

		public IActionResult ProductDetails()
		{
			var productDetails = context.ProductDetail.ToList();
			var product = context.Products.ToList();
			ViewBag.ProductDetails = productDetails;
			return View(product);
		}

		public IActionResult AddProductDetails(ProductDetails productDetails)
		{
			context.ProductDetail.Add(productDetails);
			context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult ProductDetails(int id)
		{
			var productDetails = context.ProductDetail.Where(p => p.Id == id).ToList();
			ViewBag.ProductDetails = productDetails;
			var product = context.Products.ToList();
			return View(product);
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}