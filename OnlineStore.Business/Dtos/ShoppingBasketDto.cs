using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.Dtos
{
    public class ShoppingBasketDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public UserDto User { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
      

    }
}
