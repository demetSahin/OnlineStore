using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }


    //Generic Configuration

    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            //Veritabanı üzerindeki yapılacak olan tüm sorularda aşağıdaki linq geçerli olacak.
            builder.HasQueryFilter(x => x.IsDeleted == false);

            //ModifiedDate kolonu null değer alabilir.
            builder.Property(x => x.ModifiedDate).IsRequired(false);


        }
    }
}
