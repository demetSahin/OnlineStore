using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Context;
using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OnlineStoreContext _db;
        public OrderRepository(OnlineStoreContext db)
        {
            _db = db;
        }

        public async Task DeleteOrderWithDetails(int orderId)
        {
            var order = _db.Orders
            .Include(o => o.OrderDetails)
            .FirstOrDefault(o => o.Id == orderId);

            if (order != null)
            {
                // Order'ı soft delete yap
                order.IsDeleted = true;
                order.ModifiedDate = DateTime.Now;

                // İlgili OrderDetail'leri soft delete yap
                foreach (var detail in order.OrderDetails)
                {
                    detail.IsDeleted = true;
                    detail.ModifiedDate = DateTime.Now;
                }

                // Değişiklikleri kaydet
                _db.SaveChanges();
            }
        }
    
        public async Task<List<OrderDetailEntity>> GetAllOrderDetailByOrderId(int orderId)
        {
            return await _db.OrderDetails.Where(x => x.OrderId == orderId).ToListAsync();
        }

        public async Task<List<OrderEntity>> GetAllOrdersWithUserAsync(int userId)
        {
            return await _db.Orders.Where(u => u.UserId == userId).Include(x => x.OrderDetails).ToListAsync();
        }
    }
}
