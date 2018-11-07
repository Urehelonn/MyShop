using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository productCategories;

        public ProductManagerController()
        {
            context = new ProductRepository();
            productCategories = new ProductCategoryRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(string Id)
        {
            ProductCategory product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else{
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = new Product();
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory product, string Id)
        {
            ProductCategory productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // if form invalid back to product page
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                context.Commit();

                //after edit back to main page
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        // confirm deletion
        public ActionResult Delete(ProductCategory product, string Id)
        {
            ProductCategory productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                //after edit back to main page
                return RedirectToAction("Index");
            }
        }
    }
}