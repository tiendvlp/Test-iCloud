using BusinessObject.BusinessObject;
using System;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void Create(OrderDetail orderDetail) => OrderDetailDAO.Instance.Create(orderDetail);

        public OrderDetail GetOrderDetail(int orderId, int prdId) => OrderDetailDAO.Instance.GetOrderDetail(orderId, prdId);

        public IEnumerable<OrderDetail> GetOrderDetailList() => OrderDetailDAO.Instance.GetOrderDetailList();

        public IEnumerable<OrderDetail> GetOrderDetailsByOrderId(int id) => OrderDetailDAO.Instance.GetOrderDetailsByOrderId(id);
    }
}
