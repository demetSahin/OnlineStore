using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Entities
{
    public class ProductLikeEntity:BaseEntity
    {
        public UserEntity User { get; set; }
        public int UserId { get; set; }
        public ProductEntity Product { get; set; }
        public int ProductId { get; set; }
    }

    public class ProductLikeConfiguration:BaseConfiguration<ProductLikeEntity>
    {
        public override void Configure(EntityTypeBuilder<ProductLikeEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
