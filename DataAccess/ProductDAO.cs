using BusinessObject.BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Product> GetProductList()
        {
            List<Product> products;
            try
            {
                var fStoreDb = new FStoreDBContext();
                products = fStoreDb.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = null;
            try
            {
                var fStoreDb = new FStoreDBContext();
                product = fStoreDb.Products.SingleOrDefault(prod => prod.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public IEnumerable<Product> GetProductByID(int id)
        {
            List<Product> products = new();
            try
            {
                var fStoreDb = new FStoreDBContext();
                products = fStoreDb.Products.Where(p => p.ProductId == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public IEnumerable<Product> GetProductByKeyword(String keyword)
        {
            List<Product> products = new();
            try
            {
                var fStoreDb = new FStoreDBContext();
                products = fStoreDb.Products.Where(p => p.ProductName.Contains(keyword)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }
        public IEnumerable<Product> GetProductByUnitPrice(decimal unitPrice)
        {
            List<Product> products = new();
            try
            {
                var fStoreDb = new FStoreDBContext();
                products = fStoreDb.Products.Where(p => p.UnitPrice == unitPrice).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }
        public IEnumerable<Product> GetProductByUnitInStock(int unitsInStock)
        {
            List<Product> products = new();
            try
            {
                var fStoreDb = new FStoreDBContext();
                products = fStoreDb.Products.Where(p => p.UnitsInStock == unitsInStock).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public void Create(Product product)
        {
            if (product == null || product.ProductName.Length == 0 || product.Weight.Length == 0)
            {
                throw new Exception("Please enter all fields.");
            }
            else
            {
                try
                {
                    var prod = GetProductById(product.ProductId);
                    if (prod == null)
                    {
                        var fStoreDb = new FStoreDBContext();
                        fStoreDb.Products.Add(product);
                        fStoreDb.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("This product already exists.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void Update(Product product)
        {
            if (product == null || product.ProductName.Length == 0 || product.Weight.Length == 0)
            {
                throw new Exception("Please enter all fields.");
            }
            else
            {
                try
                {
                    var prod = GetProductById(product.ProductId);
                    if (prod != null)
                    {
                        var fStoreDb = new FStoreDBContext();
                        fStoreDb.Entry<Product>(product).State = EntityState.Modified;
                        fStoreDb.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("This product doesn't exist");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void Delete(Product product)
        {
            try
            {
                var prod = GetProductById(product.ProductId);
                if (prod != null)
                {
                    var fStoreDb = new FStoreDBContext();
                    fStoreDb.Products.Remove(product);
                    fStoreDb.SaveChanges();
                }
                else
                {
                    throw new Exception("This product doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<String> GetCategoryNameList()
        {
            List<Category> categories;
            List<String> categoryNames = new List<String>();

            try
            {
                var fStoreDb = new FStoreDBContext();
                categories = fStoreDb.Categories.ToList();
                foreach (var category in categories)
                {
                    categoryNames.Add(category.CategoryName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categoryNames;
        }

        public int GetCategoryIdByName(String categoryName)
        {
            int categoryId;

            try
            {
                var fStoreDb = new FStoreDBContext();
                categoryId = fStoreDb.Categories.SingleOrDefault(category => category.CategoryName.Equals(categoryName)).CategoryId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categoryId;
        }
    }
}
