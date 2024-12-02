using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Context;
using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Repositories
{
    public class ShoppingBasketRepository : IShoppingBasketRepository
    {
        private readonly OnlineStoreContext _db;

        public ShoppingBasketRepository(OnlineStoreContext db)
        {
            _db = db;
        }
      
        

       async Task<List<ShoppingBasket>> IShoppingBasketRepository.GetAllWithProductsAndOrdersAsync()
        {
            return await _db.ShoppingBaskets.Include(x => x.Product).ToListAsync();
        }
    }
}
