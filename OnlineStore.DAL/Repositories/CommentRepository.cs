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
    public class CommentRepository:ICommentRepository
    {
        private readonly OnlineStoreContext _db;
        public CommentRepository(OnlineStoreContext db)
        {
            _db = db;
        }

        public  async Task DeleteComment(int id)
        {
            
            var entity = await  _db.Comments
                .Include(p => p.Product)
                .Include(u => u.User)
                .FirstOrDefaultAsync(c => c.Id == id);
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == entity.UserId);
            var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == entity.ProductId);
            entity.IsDeleted = true;
            entity.ModifiedDate = DateTime.Now;
           
            _db.Users.Update(user);
            _db.Products.Update(product);
            _db.Comments.Update(entity);
            await _db.SaveChangesAsync();


        }

        public async Task DeleteProductWithComments(int productId)
        {
            var comments = GetCommentsByProductId(productId).ToList();
            foreach (var entity in comments)
            {
                entity.IsDeleted = true;
                entity.ModifiedDate = DateTime.Now;
                _db.Comments.Update(entity);
              
            }

            var product = _db.Products.Find(productId);
            product.IsDeleted = true;
            product.ModifiedDate = DateTime.Now;
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }

        public IQueryable<CommentEntity> GetCommentsByProductId(int productId)
        {
            return _db.Comments
         .Where(c => c.ProductId == productId)
         .Include(c => c.User)
         .Include(c => c.Product);
        }
    }
}
