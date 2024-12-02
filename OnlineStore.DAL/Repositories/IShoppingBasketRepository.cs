using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Repositories
{
    public interface IShoppingBasketRepository
    {
        Task<List<ShoppingBasket>> GetAllWithProductsAndOrdersAsync();
    }
}
