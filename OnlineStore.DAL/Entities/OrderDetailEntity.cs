using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Entities
{
    public class OrderDetailEntity:BaseEntity
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public ProductEntity Product { get; set; }
        public OrderEntity Order { get; set; }
        public decimal? Price { get; set; }

    }

    public class OrderDetailConfiguration:BaseConfiguration<OrderDetailEntity>
    {
        public override void Configure(EntityTypeBuilder<OrderDetailEntity> builder)
        {
            builder.Property(x => x.Price)
                .IsRequired(false);

            base.Configure(builder);
        }
    }
}
