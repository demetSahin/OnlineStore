using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Context
{
    public class OnlineStoreContext:DbContext
    {
        private readonly IDataProtector _dataProtector;
        public OnlineStoreContext(DbContextOptions<OnlineStoreContext>options, IDataProtectionProvider dataProtectionProvider):base(options)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }

        //modelBuilder.Entity<Blog>().HasMany(b => b.Posts).WithOne(p => p.Blog).IsRequired(false);
        //modelBuilder.Entity<Blog>().HasQueryFilter(b => b.Url.Contains("fish"));
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
           

            modelBuilder.Entity<OrderEntity>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);  


            modelBuilder.Entity<OrderDetailEntity>()
                .HasOne(p => p.Order)
                .WithMany(u => u.OrderDetails)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderDetailEntity>()
                .HasOne(p => p.Product)
                .WithMany(u => u.OrderDetails)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
           
            modelBuilder.Entity<ProductLikeEntity>()
                 .HasOne(p => p.User)
                 .WithMany(u => u.ProductLikes)
                 .HasForeignKey(p => p.UserId)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductLikeEntity>()
                .HasOne(p => p.Product)
                .WithMany(u => u.ProductLikes)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CommentEntity>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CommentEntity>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductLikeConfiguration());
            modelBuilder.ApplyConfiguration(new ShoppingBasketConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            




            var pwd = "1234";
            pwd = _dataProtector.Protect(pwd);

            //SEED DATA
            modelBuilder.Entity<UserEntity>().HasData(new List<UserEntity>() 
            {
                new UserEntity()
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@gmail.com",
                    Password = pwd,
                    UserType = Enums.UserTypeEnum.Admin
                }
            
            
            });

            modelBuilder.Entity<CategoryEntity>().HasData(
                new CategoryEntity()
                {
                    Id = 1,
                    Name = "Kişisel Bakım",
                    Description = "Kişisel bakım ürünleri burada"
                });

            modelBuilder.Entity<ProductEntity>().HasData(
               new ProductEntity()
               {
                   Id = 1,
                   Name = "Parfüm",
                   Description = "Chanel",
                   UnitPrice = 6000,
                   UnitsInStock = 5,
                   ImagePath = "ChanelGabrielle-f1ad9a4c-9005-477d-832f-41b0399eae11-f2f07cbc-1153-4dcf-8170-e4140c2fe0bf.jpg",
                   CategoryId = 1,
                   LikeCount = 0,

               });




            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<OrderDetailEntity> OrderDetails => Set<OrderDetailEntity>();
        public DbSet<ShoppingBasket> ShoppingBaskets => Set<ShoppingBasket>();
        public DbSet<ProductLikeEntity> ProductLikes => Set<ProductLikeEntity>();
        public DbSet<CommentEntity> Comments => Set<CommentEntity>();

    }
}
