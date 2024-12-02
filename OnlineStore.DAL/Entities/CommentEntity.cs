using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Entities
{
    public class CommentEntity: BaseEntity
    {
            public int ProductId { get; set; } // İlişkili ürün
            public ProductEntity Product { get; set; } // Navigasyon property

            public int UserId { get; set; } // Yorumu yapan kullanıcı
            public UserEntity User { get; set; } // Navigasyon property

            public string Content { get; set; } // Yorum içeriği
            public int Rating { get; set; } // 1-5 arası puanlama
       
    }

    public class CommentConfiguration:BaseConfiguration<CommentEntity>
    {
        public override void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
