using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Entities
{
    public class ProductEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
        public List<OrderDetailEntity> OrderDetails { get; set; }
        public List<ProductLikeEntity> ProductLikes { get; set; }
        public List<CommentEntity> Comments { get; set; }

        public int LikeCount { get; set; }
    }

    public class ProductConfiguration : BaseConfiguration<ProductEntity>
    {
        public override void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(75);

            builder.Property(x => x.Description)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(x =>x.UnitPrice)
                .IsRequired(false);

            builder.Property(x => x.ImagePath) 
                .IsRequired(false);


            base.Configure(builder);


        }
    }
}
