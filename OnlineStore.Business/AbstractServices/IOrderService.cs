using OnlineStore.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.AbstractServices
{
    public interface IOrderService
    {
        List<OrderListDto> GetUserOrders(int? userId = null);
        OrderDto GetOrder();
        List<OrderListDto> GetAllOrders();
        Task<OrderUpdateDto> GetOrderById(int id);

        Task Update(OrderUpdateDto orderUpdateDto);
        Task DeleteOrder(int id);

    }
}
