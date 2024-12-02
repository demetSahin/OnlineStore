using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Repositories
{
    public interface ICommentRepository
    {
        IQueryable<CommentEntity> GetCommentsByProductId(int productId);
        Task DeleteProductWithComments(int productId);
        Task DeleteComment(int commentId);
    }
}
