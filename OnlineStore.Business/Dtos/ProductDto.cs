using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public List<ProductLikeDto>? ProductLikeDtos { get; set; }
        public int? LikeCount { get; set; }
        public UserDto UserDto { get; set; }
        public int UserId { get; set; }
        public decimal? UnitPrice { get; set; }
        public List<CommentDto>? CommentDtos { get; set; }
       

    }
}
