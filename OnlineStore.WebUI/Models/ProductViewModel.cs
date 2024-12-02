using OnlineStore.Business.Dtos;

namespace OnlineStore.WebUI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
        public List<ProductLikeViewModel> ProductLikeViewModels { get; set; }
        public List<CommentViewModel> CommentViewModels { get; set; }

        public int LikeCount { get; set; }
       
        public int UserId { get; set; }
    }
}
