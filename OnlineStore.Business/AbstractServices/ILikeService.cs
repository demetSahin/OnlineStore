using OnlineStore.Business.Dtos;
using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.AbstractServices
{
    public interface ILikeService
    {
        IQueryable<ProductLikeEntity> GetAllLikes(); // Tüm beğenileri getirir
        IQueryable<ProductLikeEntity> GetLikesByProduct(int productId); // Belirli bir ürünün beğenilerini getirir
        Task<bool> UserHasLikedAsync(int productId, int userId); // Kullanıcının belirli bir ürünü beğenip beğenmediğini kontrol eder
        Task LikeProductAsync(int productId, int userId); // Kullanıcı bir ürünü beğenir
        Task UnlikeProductAsync(int productId, int userId); // Kullanıcı bir ürünü beğenmekten vazgeçer
        Task<List<UserLikedProductDto>> GetUserLikedProductsAsync(int userId);//Kullanıcının beğendiği ürünleri getirir.
    }
}
