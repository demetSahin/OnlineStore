using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Entities
{
    public class ShoppingBasket : BaseEntity
    {
        public int UserId { get; set; }

        public UserEntity User { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }



    }
    public class ShoppingBasketConfiguration : BaseConfiguration<ShoppingBasket>
    {
        public override void Configure(EntityTypeBuilder<ShoppingBasket> builder)
        {
            base.Configure(builder);
        }
    }

}  
