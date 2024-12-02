using AutoMapper;
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
    public class ProductService : IProductService
    {
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<ProductLikeEntity> _likeRepository;
        private readonly IRepository<CommentEntity> _commentRepository;
        private readonly ICommentService _commentService;
        private readonly ICommentRepository _commentRepo;

        public ProductService(IRepository<ProductEntity> productRepository,IRepository<UserEntity> userRepository,IMapper mapper, IRepository<ProductLikeEntity> likeRepository, IRepository<CommentEntity> commentRepository, ICommentService commentService,ICommentRepository commentRepo)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _likeRepository = likeRepository;
            _commentRepository = commentRepository;
            _commentService = commentService;
            _commentRepo = commentRepo;
        }
        
        public async Task AddProduct(ProductAddDto productAddDto)
        {
            var entity = new ProductEntity()
            {
                Name = productAddDto.Name,
                Description = productAddDto.Description,
                UnitPrice = productAddDto.UnitPrice,
                UnitsInStock = productAddDto.UnitsInStock,
                CategoryId = productAddDto.CategoryId,
                ImagePath = productAddDto.ImagePath,
            };

            await _productRepository.AddAsync(entity);
        }

        public async Task CommentProduct(int productId, int userId, string commentText, int rating)
        {
            var product = await _productRepository.GetByIdAsync(productId);


            if (product == null)
            {
                throw new Exception("Ürün mevcut değil.");
            }
            var user = await _userRepository.GetByIdAsync(userId);


            var comment = _mapper.Map<CommentDto>(new CommentEntity());
            comment.UserId = userId;
            comment.ProductId = productId;
            comment.Content = commentText;
            comment.Rating = rating;
            comment.ProductDto = _mapper.Map<ProductDto>(product);
            comment.UserDto = _mapper.Map<UserDto>(user);

            var comments = _commentRepository.GetAll();

            var commentedProduct = comments.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);

            if (commentedProduct == null)//kullanıcı daha önce yorum yapmadıysa
            {
                await _commentRepository.AddAsync(_mapper.Map<CommentEntity>(comment));
                
                await _productRepository.UpdateAsync(product);
                await _userRepository.UpdateAsync(user);

            }
        }

        public  async Task DeleteProduct(int id)
        {
            var comments = _commentRepository.GetAll().Where(x => x.ProductId == id).ToList();
          
            foreach (var item in comments)
            {
                item.IsDeleted = true;
                item.ModifiedDate = DateTime.UtcNow;
                await _commentRepository.UpdateAsync(item);
            }
            await _productRepository.DeleteAsync(id);
        }

        public async  Task DeleteProductComment(int productId,int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var product = await _productRepository.GetByIdAsync(productId);
            var comments = _commentRepository.GetAll();

            var comment = comments.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);
            if(comment != null)
            {
                await _commentRepository.DeleteAsync(comment.Id);

                await _productRepository.UpdateAsync(product);
                
            }
        }

        public async Task DeleteProductWithComments(int id)
        {
            await _commentRepo.DeleteProductWithComments(id);

        }

        

        public async Task<ProductUpdateDto> GetProductById(int id)
        {
            var entity = await _productRepository.GetByIdAsync(id);

            var productUpdateDto = new ProductUpdateDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                UnitPrice = entity.UnitPrice,
                UnitsInStock = entity.UnitsInStock,
                CategoryId = entity.CategoryId,
                ImagePath = entity.ImagePath,
            };

            return productUpdateDto;
        }
        
        public ProductDetailDto GetProductDetail(int id)
        {
            var entity = _productRepository.GetEntity(id);
            var comments = _commentRepo.GetCommentsByProductId(id).ToList();

            var productDetailDto = new ProductDetailDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                UnitPrice = entity.UnitPrice,
                UnitsInStock = entity.UnitsInStock,
                ImagePath = entity.ImagePath,
                CommentDtos = _mapper.Map<List<CommentDto>>(entity.Comments),
                LikeCount = entity.LikeCount,
                ProductLikeDtos = _mapper.Map<List<ProductLikeDto>>(entity.ProductLikes),



            };
            if (comments.Any())
            {
                productDetailDto.AverageRating = comments.Average(c => c.Rating); // Ortalama puan
                productDetailDto.TotalComments = comments.Count; // Yorum sayısı
            }

            return productDetailDto;
        }

        public async Task<List<ProductListDto>> GetProducts()
        {
            var productEntites = _productRepository.GetAll();
                productEntites.OrderBy(x => x.Category.Name).ThenBy(x => x.Name);
            // Önce kategori adına sonra kategori içinde ürün isimlerine göre sıralıyorum.

            var productDtoList = productEntites.Select(x => new ProductListDto()
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                CategoryName = x.Category.Name,
                ImagePath = x.ImagePath

            }).ToList();

            return productDtoList;
        }

        public List<ProductListDto> GetProductsByCategoryId(int? categoryId = null)
        {
            if (categoryId.HasValue) // is not null
            {
                var productEntities = _productRepository.GetAll(x => x.CategoryId == categoryId).OrderBy(x => x.Name);
                // Gönderdiğim categoryId ile entity'deki categoryId'si eşleşen ürünleri isimlerine göre sıralayarak getir.

                var productDtos = productEntities.Select(x => new ProductListDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UnitPrice = x.UnitPrice,
                    UnitsInStock = x.UnitsInStock,
                    CategoryName = x.Category.Name,
                    ImagePath = x.ImagePath
                    
                }).ToList();

                return productDtos;
            }

            var productEntites = _productRepository.GetAll().OrderBy(x => x.Category.Name).ThenBy(x => x.Name);
            // Önce kategori adına sonra kategori içinde ürün isimlerine göre sıralıyorum.

            var productDtoList = productEntites.Select(x => new ProductListDto()
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                CategoryName = x.Category.Name,
                ImagePath = x.ImagePath
            }).ToList();

            return productDtoList;
        }


        public async Task LikeProduct(int productId, int userId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

          
            if (product == null)
            {
                throw new Exception("Ürün mevcut değil.");
            }
            var user = await _userRepository.GetByIdAsync(userId);


            var like = _mapper.Map<ProductLikeDto>(new ProductLikeEntity());
            like.UserId = userId;
            like.ProductId = productId;
            like.ProductDto = _mapper.Map<ProductDto>(product);
            like.UserDto = _mapper.Map<UserDto>(user);

            var likes =  _likeRepository.GetAll();

            var likedProduct = likes.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);

            if(likedProduct == null)//kullanıcı daha önce beğenmediyse beğen
            {
                await _likeRepository.AddAsync(_mapper.Map<ProductLikeEntity>(like));
                product.LikeCount++;
                await _productRepository.UpdateAsync(product);

            }
            else //öncesinde beğendiyse beğeniyi geri çek
            {
                await _likeRepository.DeleteAsync(likedProduct.Id);
                product.LikeCount--;
                await _productRepository.UpdateAsync(product);

            }


        }

        public async Task<List<ProductDto>> Search(string searchedTerm)
        {
            var allProducts = await _productRepository.GetAllAsync();
            var productList = new List<ProductDto>();
            foreach (var product in allProducts)
            {
                if (product.Name.Contains(searchedTerm))
                {
                    productList.Add(_mapper.Map<ProductDto>(product));
                }
            }
            return productList;
        }

        public async Task UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            var entity = await _productRepository.GetByIdAsync(productUpdateDto.Id);

            entity.Name = productUpdateDto.Name;
            entity.Description = productUpdateDto.Description;
            entity.UnitPrice = productUpdateDto.UnitPrice;
            entity.UnitsInStock = productUpdateDto.UnitsInStock;
            entity.CategoryId = productUpdateDto.CategoryId;

            if (productUpdateDto.ImagePath != "")
                entity.ImagePath = productUpdateDto.ImagePath;
            // Bu if'i yazmazsam, productUpdateDto ile View'den gelen boş olan string ImagePath bilgisi, veritabanındaki görsel üzerinde yazılır. Bu durumda görseli kaybederim. 

           await _productRepository.UpdateAsync(entity);
        }


       

    }
}
