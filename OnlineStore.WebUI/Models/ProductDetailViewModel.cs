using OnlineStore.DAL.Entities;

namespace OnlineStore.WebUI.Models
{
    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int? LikeCount { get; set; }
        public bool UserHasCommented { get; set; }
        public bool UserHasLiked { get; set; }
        public List<ProductLikeViewModel> ProductLikeViewModels { get; set; }
        public List<CommentViewModel> CommentViewModels { get; set; }
        public double? AverageRating { get; set; } // Nullable: Yorum yoksa null
        public int? TotalComments { get; set; }   // Nullable: Yorum yoksa null

    }
}
