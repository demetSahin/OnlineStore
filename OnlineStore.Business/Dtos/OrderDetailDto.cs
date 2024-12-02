using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.Dtos
{
    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public ProductDto Product { get; set; }
        public OrderDto Order { get; set; }
    }
}
