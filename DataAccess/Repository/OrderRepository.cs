using BusinessObject.BusinessObject;
using System;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void Create(Order order)=> OrderDAO.Instance.Create(order);

        public void Delete(Order order)=> OrderDAO.Instance.Delete(order);

        public Order GetOrderById(int id) => OrderDAO.Instance.GetOrderById(id);

        public IEnumerable<Order> GetOrderList() => OrderDAO.Instance.GetOrderList();

        public IEnumerable<Order> GetOrdersByDate(DateTime startDate, DateTime endDate) => OrderDAO.Instance.GetOrdersByDate(startDate, endDate);

        public IEnumerable<Order> GetOrdersByMemberId(int id) => OrderDAO.Instance.GetOrdersByMemberId(id);

        public IEnumerable<Sales> GetStatistic(DateTime startDate, DateTime endDate) => OrderDAO.Instance.GetStatistic(startDate, endDate);

        public void Update(Order order) => OrderDAO.Instance.Update(order);
    }
}
