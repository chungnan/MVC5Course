﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using Omu.ValueInjecter;

namespace MVC5Course.Controllers
{
    public class ProductsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: Products
        public ActionResult Index()
        {
            var data = db.Product
                .OrderByDescending(p => p.ProductId)
                .Take(10)
                .ToList();

            return View(data);
        }

        public ActionResult Index2()
        {
            var data = db.Product
                .Where(w => w.Active == true)
                .OrderByDescending(o => o.ProductId)
                .Take(10)
                .Select(s => new ProductViewModel()
                {
                    ProductId = s.ProductId,
                    ProductName = s.ProductName,
                    Price = s.Price,
                    Stock = s.Stock
                });

            return View(data);
        }

        public ActionResult AddNewProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewProduct(ProductViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // 新增 Product 資料
            var product = new Product()
            {
                // ProductId 由 db table 自動編號
                ProductId = data.ProductId,
                ProductName = data.ProductName,
                Price = data.Price,
                Active = true,
                Stock = data.Stock
            };

            db.Product.Add(product);
            db.SaveChanges();

            return RedirectToAction("Index2");
        }

        public ActionResult EditProduct(int id)
        {
            var data = db.Product.Find(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult EditProduct(int id, ProductViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            var product = db.Product.Find(id);
            //product.ProductName = data.ProductName;
            //product.Price = data.Price;
            //product.Stock = data.Stock;

            product.InjectFrom(data);

            db.SaveChanges();

            return RedirectToAction("Index2");
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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
