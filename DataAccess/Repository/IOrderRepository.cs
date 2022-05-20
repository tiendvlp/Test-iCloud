using BusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrderList();
        IEnumerable<Order> GetOrdersByMemberId(int id);
        IEnumerable<Order> GetOrdersByDate(DateTime startDate, DateTime endDate);
        Order GetOrderById(int id);
        void Create(Order order);
        void Update(Order order);
        void Delete(Order order);
        IEnumerable<Sales> GetStatistic(DateTime startDate, DateTime endDate);
    }
}
