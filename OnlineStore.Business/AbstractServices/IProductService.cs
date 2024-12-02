using OnlineStore.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.AbstractServices
{
    public interface IProductService
    {
        Task AddProduct(ProductAddDto productAddDto);
        Task<List<ProductListDto>>GetProducts();
        Task<ProductUpdateDto> GetProductById(int id);
        Task UpdateProduct(ProductUpdateDto productUpdateDto);
        Task DeleteProduct(int id);
        List<ProductListDto> GetProductsByCategoryId(int? categoryId = null);
        ProductDetailDto GetProductDetail(int id);
        Task LikeProduct(int productId, int userId);
        Task CommentProduct(int productId, int userId, string commentText, int rating);
        Task DeleteProductComment(int productId, int userId);
        Task<List<ProductDto>> Search(string searchedTerm);
        Task DeleteProductWithComments(int id);

    }
}
