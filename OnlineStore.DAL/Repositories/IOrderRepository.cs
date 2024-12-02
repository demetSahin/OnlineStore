using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Repositories
{
    public interface IOrderRepository
    {
        Task<List<OrderEntity>> GetAllOrdersWithUserAsync(int userId);
        Task<List<OrderDetailEntity>> GetAllOrderDetailByOrderId(int orderId);
        Task DeleteOrderWithDetails(int id);

    }
}
