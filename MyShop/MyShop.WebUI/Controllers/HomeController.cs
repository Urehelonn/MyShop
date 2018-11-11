using MyShop.Core.Contract;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoriesContext)
        {
            context = productContext;
            productCategories = productCategoriesContext;
        }

        public ActionResult Index(string Category=null)
        {
            List<Product> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();
            if (Category == null)
            {
                products = context.Collection().ToList();
            }
            else{
                // if there is category restriction, filter only prodcut with the required category
                products = context.Collection().Where(p => p.Category==Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
    }
}