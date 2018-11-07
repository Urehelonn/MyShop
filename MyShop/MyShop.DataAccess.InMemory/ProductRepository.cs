using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> products = new List<ProductCategory>();

        public ProductRepository()
        {
            products = cache["products"] as List<ProductCategory>;
            if (products == null)
            {
                products = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            // save curr products to cache
            cache["products"] = products;
        }

        public void Insert(ProductCategory p)
        {
            products.Add(p);
        }

        public void Update(ProductCategory product)
        {
            ProductCategory productToUpdate = products.Find(p => p.Id == product.Id);
            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not Found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory product = products.Find(p => p.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not Found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productToDelete = products.Find(p => p.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not Found");
            }

        }
    }
}
