using BusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProductList();
        Product GetProductById(int id);
        IEnumerable<Product> GetProductByID(int id);
        IEnumerable<Product> GetProductByKeyword(String keyword);
        IEnumerable<Product> GetProductByUnitPrice(decimal unitPrice);
        IEnumerable<Product> GetProductByUnitInStock(int unitsInStock);
        void Create(Product product);
        void Update(Product product);
        void Delete(Product product);
        IEnumerable<Category> GetCategoryList();
    }
}
