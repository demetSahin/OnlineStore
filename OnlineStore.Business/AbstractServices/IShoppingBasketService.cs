using OnlineStore.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.AbstractServices
{
    public interface IShoppingBasketService
    {
        Task AddToBasket(ShoppingBasketDto shoppingBasketDto);
        Task<List<ShoppingBasketDto>> GetShoppingBasket();
        Task DeleteFromBasket(int id);
        Task <decimal> GetTotalPrice();

        Task<bool> SaveOrder(OrderAddDto orderDto);
        Task AddOrder(OrderAddDto orderAddDto);
        Task AddOrderDetail(OrderDetailDto orderDetailDto);
    }
}
