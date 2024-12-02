using Microsoft.EntityFrameworkCore;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.Dtos;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.ConcreteServices
{
    public class LikeService : ILikeService
    {
        private readonly IRepository<ProductLikeEntity> _productLikeRepository;
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly ILikeRepository _productLikeRepo;

        public LikeService(IRepository<ProductLikeEntity>  productLikeRepository,IRepository<ProductEntity> productRepository,IRepository<UserEntity> userRepository,ILikeRepository productLikeRepo)
        {
            _productLikeRepository = productLikeRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _productLikeRepo = productLikeRepo;
        }

        public IQueryable<ProductLikeEntity> GetAllLikes()
        {
            return _productLikeRepository.GetAll();
        }

        public IQueryable<ProductLikeEntity> GetLikesByProduct(int productId)
        {
            return _productLikeRepo.GetLikesByProductIdAsync(productId);
        }

        public async Task<bool> UserHasLikedAsync(int productId, int userId)
        {
            return await _productLikeRepository.GetAll()
                .AnyAsync(pl => pl.ProductId == productId && pl.UserId == userId);
        }

        public async Task LikeProductAsync(int productId, int userId)
        {
            if (!await UserHasLikedAsync(productId, userId))
            {
                var productLike = new ProductLikeEntity
                {
                    ProductId = productId,
                    UserId = userId,
                   
                    
                };

                await _productLikeRepository.AddAsync(productLike);
            }
        }

        public async Task UnlikeProductAsync(int productId, int userId)
        {
            var productLike = _productLikeRepository.GetAll()
                .Where(pl => pl.ProductId == productId && pl.UserId == userId)
                .FirstOrDefault();

            if (productLike != null)
            {
                await _productLikeRepository.DeleteAsync(productLike);
            }
        }

        public async Task<List<UserLikedProductDto>> GetUserLikedProductsAsync(int userId)
        {
            var productLikes = await _productLikeRepo.GetProductLikesByUserIdAsync(userId);
            var userLikedProducts = productLikes.Select(x => new UserLikedProductDto()
            {
                Id = x.ProductId,
                Name = x.Product.Name,
                UnitPrice = x.Product.UnitPrice,
                ImagePath = x.Product.ImagePath,
                UnitsInStock = x.Product.UnitsInStock


            }).ToList();
            return userLikedProducts;

        }
    }
    
}
