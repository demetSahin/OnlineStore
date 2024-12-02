using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Context;
using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly OnlineStoreContext _db;
        
        public LikeRepository(OnlineStoreContext db)
        {
            _db = db;
           
        }
  

        public IQueryable<ProductLikeEntity> GetLikesByProductIdAsync(int productId)
        {
            return _db.ProductLikes.Where(pl=>pl.ProductId == productId);
        }

        public async Task<List<ProductLikeEntity>> GetProductLikesByUserIdAsync(int userId)
        {
            return await _db.ProductLikes
           .Where(pl => pl.UserId == userId && !pl.IsDeleted)
           .Include(pl => pl.Product) // Product'ı dahil et
           
           .ToListAsync();
        }

        

       
    }
}
