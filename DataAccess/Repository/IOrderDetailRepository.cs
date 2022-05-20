using BusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetOrderDetailList();
        IEnumerable<OrderDetail> GetOrderDetailsByOrderId(int id);
        OrderDetail GetOrderDetail(int orderId, int prdId);
        void Create(OrderDetail orderDetail);
    }
}
