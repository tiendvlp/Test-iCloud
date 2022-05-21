using BusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void Create(Product product) => ProductDAO.Instance.Create(product);

        public void Delete(Product product) => ProductDAO.Instance.Delete(product);

        public IEnumerable<Category> GetCategoryList() => ProductDAO.Instance.GetCategoryList();

        public Product GetProductById(int id) => ProductDAO.Instance.GetProductById(id);

        public IEnumerable<Product> GetProductByID(int id) => ProductDAO.Instance.GetProductByID(id);

        public IEnumerable<Product> GetProductByKeyword(string keyword) => ProductDAO.Instance.GetProductByKeyword(keyword);

        public IEnumerable<Product> GetProductByUnitInStock(int unitsInStock) => ProductDAO.Instance.GetProductByUnitInStock(unitsInStock);

        public IEnumerable<Product> GetProductByUnitPrice(decimal unitPrice) => ProductDAO.Instance.GetProductByUnitPrice(unitPrice);

        public IEnumerable<Product> GetProductList() => ProductDAO.Instance.GetProductList();

        public void Update(Product product) => ProductDAO.Instance.Update(product);
    }
}
