using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.Dtos
{
    public class ProductLikeDto
    {
       
        public UserDto UserDto { get; set; }
        public int UserId { get; set; }
        public ProductDto ProductDto { get; set; }
        public int ProductId { get; set; }
    }
}
