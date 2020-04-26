﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RAQ_Store.Models;
using RAQ_Store.ViewModels;

namespace RAQ_Store.Controllers
{
    public class ProductsController : Controller
    {
        private StoreContext db = new StoreContext();


        public ActionResult ViewProduct()
        {

            ProductCategory prca = new ProductCategory
            {
                Product = db.Products.ToList(),
                Category = db.Categories.ToList(),
                cart = db.Cart.ToList()
            };
            return View(prca);
        }

        [HttpGet]
        public ActionResult AddCart(int ID, DateTime time)
        {
            Cart adcrt = new Cart
            { added_at = time,
                product_id = ID
            };
            ProductCategory prca = new ProductCategory
            {
                Mycart = adcrt
            };

            db.Cart.Add(prca.Mycart);

            db.SaveChanges();

            return RedirectToAction("ViewProduct");
        }
        [HttpGet]
        public ActionResult AddProduct()
        {

            ProductCategory prca = new ProductCategory
            {
                Category = db.Categories.ToList()
            };

            return View(prca);
        }
        [HttpGet]
        public ActionResult RemoveCart(int ID)
        {

            var car = db.Cart.Single(c => c.product_id == ID);
            db.Cart.Remove(car);
            db.SaveChanges();

            return RedirectToAction("ViewProduct");

        }
        [HttpPost]
        public ActionResult AddProduct(ProductCategory prca)
        {
            db.Products.Add(prca.MyProduct);

            db.SaveChanges();
            return RedirectToAction("ViewProduct");
        }
    
        public ActionResult Cart()
        {
            ProductCategory prca = new ProductCategory
            {
                Product = db.Products.ToList(),
                cart =db.Cart.ToList()
            };

            return View(prca);
        }
      


        [HttpGet]
        public ActionResult Details(int ID)
        {
            var product = db.Products.ToList().Single(c => c.id == ID);

            ProductCategory prca = new ProductCategory
            {
                MyProduct = product,
                MyCategory = db.Categories.ToList().Single(c => c.id == product.category_id),
            };


            return View(prca);
        }



        [HttpGet]
        public ActionResult Update(int ID)
        {

            var product = db.Products.ToList().Single(c => c.id == ID);

            ProductCategory prca = new ProductCategory
            {
                MyProduct = product,
                MyCategory = db.Categories.ToList().Single(c => c.id == product.category_id),
                Category = db.Categories.ToList()
            };


            return View(prca);
        }
        [HttpPost]
        public ActionResult Update(ProductCategory prca)
        {
            if (!ModelState.IsValid)
            {
                var mcat = db.Categories.ToList();
                prca.Category = mcat;
                return View("Update", prca);
            }

            var productDb = db.Products.ToList().Single(c => c.id == prca.MyProduct.id);
            productDb.name = prca.MyProduct.name;
            productDb.price = prca.MyProduct.price;
            productDb.category_id = prca.MyProduct.category_id;
            productDb.description = prca.MyProduct.description;
            productDb.image = prca.MyProduct.image;
            db.SaveChanges();



            return RedirectToAction("ViewProduct");


        }
        [HttpGet]
        public ActionResult Delete(int ID)
        {

            var product = db.Products.Single(c => c.id == ID);
            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("ViewProduct");

        }
      
    }
}


        /**
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "id", "name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,price,image,description,category_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Categories, "id", "name", product.category_id);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Categories, "id", "name", product.category_id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,price,image,description,category_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.Categories, "id", "name", product.category_id);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
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
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
}**/