using BusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO _instance = null;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new OrderDetailDAO();
                    }
                    return _instance;
                }
            }
        }

        public IEnumerable<OrderDetail> GetOrderDetailList()
        {
            List<OrderDetail> orderDetails;
            try
            {
                var db = new FStoreDBContext();
                orderDetails = db.OrderDetails.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetails;
        }

        public IEnumerable<OrderDetail> GetOrderDetailsByOrderId(int id)
        {
            List<OrderDetail> orderDetails;
            try
            {
                var db = new FStoreDBContext();
                orderDetails = db.OrderDetails.Where(order => order.OrderId == id)
                                            .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetails;
        }

        public OrderDetail GetOrderDetail(int orderId, int prdId)
        {
            OrderDetail orderDetail = null;
            try
            {
                var db = new FStoreDBContext();
                orderDetail = db.OrderDetails.SingleOrDefault(
                    order => order.OrderId == orderId && order.ProductId == prdId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        public void Create(OrderDetail orderDetail)
        {
            try
            {
                var order = GetOrderDetail(orderDetail.OrderId, orderDetail.ProductId);
                if (order != null)
                {
                    var db = new FStoreDBContext();
                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("This detail doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
