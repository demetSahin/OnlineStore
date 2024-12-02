using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Entities
{
    public class OrderEntity : BaseEntity
    {
        public decimal? TotalPrice { get; set; }
        public string Fullname { get; set; }
        public string AddressName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public List<OrderDetailEntity> OrderDetails { get; set; }
    }
    public class OrderConfiguration:BaseConfiguration<OrderEntity>
    {
        public override void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.Property(x => x.TotalPrice)
                .IsRequired(false);
            base.Configure(builder);
        }
    }
}
