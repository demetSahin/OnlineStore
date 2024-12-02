using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.Dtos
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int? LikeCount { get; set; }
        public string ImagePath { get; set; }
        public bool UserHasLiked { get; set; }
        public bool UserHasCommented { get; set; }
        public List<ProductLikeDto>? ProductLikeDtos { get; set; }
        public List<CommentDto>? CommentDtos { get; set; }

        public double? AverageRating { get; set; } // Nullable: Yorum yoksa null
        public int? TotalComments { get; set; }   // Nullable: Yorum yoksa null


    }
}
