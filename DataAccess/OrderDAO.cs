using BusinessObject.BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO _instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new OrderDAO();
                    }
                    return _instance;
                }
            }
        }

        public IEnumerable<Order> GetOrderList()
        {
            List<Order> orders;
            try
            {
                var db = new FStoreDBContext();
                orders = db.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }

        public IEnumerable<Order> GetOrdersByMemberId(int id)
        {
            List<Order> orders;
            try
            {
                var db = new FStoreDBContext();
                orders = db.Orders.Where(o => o.MemberId == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }

        public IEnumerable<Order> GetOrdersByDate(DateTime startDate, DateTime endDate)
        {
            List<Order> orders;
            try
            {
                var db = new FStoreDBContext();
                orders = db.Orders.Where(o => o.OrderDate > startDate && o.OrderDate < endDate)
                                  .OrderByDescending(o => o.OrderDate)
                                  .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }

        public Order GetOrderById(int id)
        {
            Order order = null;
            try
            {
                var db = new FStoreDBContext();
                order = db.Orders.SingleOrDefault(o => o.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public void Create(Order order)
        {
            if (order == null)
            {
                throw new Exception("Please enter all fields.");
            }
            try
            {
                var ord = GetOrderById(order.OrderId);
                if (ord == null)
                {
                    var db = new FStoreDBContext();
                    db.Orders.Add(order);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("This order already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Order order)
        {
            try
            {
                var ord = GetOrderById(order.OrderId);
                if (ord != null)
                {
                    var db = new FStoreDBContext();
                    db.Entry<Order>(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("This order doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Order order)
        {
            try
            {
                var ord = GetOrderById(order.OrderId);
                if (ord != null)
                {
                    var db = new FStoreDBContext();
                    db.Orders.Remove(ord);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("This order doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Sales> GetStatistic(DateTime startDate, DateTime endDate)
        {
            List<Order> orders;
            List<Sales> sales = new();
            try
            {
                var db = new FStoreDBContext();
                orders = db.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                                    .Include(o => o.OrderDetails)
                                    .OrderByDescending(o => o.OrderDate)
                                    .ToList();

                foreach (Order order in orders)
                {
                    decimal amount = order.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * ((decimal)(100 - od.Discount) / 100));
                    sales.Add(new Sales()
                    {
                        Date = order.OrderDate,
                        Amount = amount
                    });
                }

                sales = sales.OrderByDescending(s => s.Amount).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return sales;
        }
    }
}
