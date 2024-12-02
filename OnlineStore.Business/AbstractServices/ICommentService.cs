using OnlineStore.Business.Dtos;
using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.AbstractServices
{
    public interface ICommentService
    {
        IQueryable<CommentEntity> GetAllComments(); // Tüm yorumları getirir
        IQueryable<CommentEntity> GetCommentsByProduct(int productId); // Belirli bir ürünün yorumlarını getirir
        bool UserHasCommented(int productId, int userId); // Kullanıcının belirli bir ürüne yorum yapıp yapmadığını kontrol eder
       
        Task DeleteProductCommentAsync(int productId, int userId); // Kullanıcı bir ürüne yorum yapmaktan vazgeçer
        Task DeleteProductsAllComments(int productId);
        Task DeleteComment(int id);
        List<CommentListDto> GetComments();
    }
}
