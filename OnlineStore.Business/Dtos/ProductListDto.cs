using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.Dtos
{
    public class ProductListDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
        public int? LikeCount { get; set; }
        public List<ProductLikeDto>? ProductLikeDtos { get; set; }
        public List<CommentDto>? CommentDtos { get; set; }

      
    }
}
