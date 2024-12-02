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
using System.Xml.Linq;

namespace OnlineStore.Business.ConcreteServices
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<CommentEntity> _commentRepository;
        private readonly ICommentRepository _commentRepo;
        private readonly IMapper _mapper;
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly IRepository<UserEntity> _userRepository;

        public CommentService(IRepository<CommentEntity> commentRepository, ICommentRepository commentRepo,IMapper mapper,IRepository<ProductEntity> productRepository, IRepository<UserEntity> userRepository)
        {
            _commentRepository = commentRepository;
            _commentRepo = commentRepo;
            _mapper = mapper;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        

        public async Task DeleteComment(int id)
        {
           await _commentRepo.DeleteComment(id);
            
        }

        public async Task DeleteProductCommentAsync(int productId, int userId)
        {
         
            var comments = _commentRepository.GetAll();
            var comment = comments.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);
            await _commentRepo.DeleteComment(comment.Id);
            

        }

        public async Task DeleteProductsAllComments(int productId)
        {
            var comments = _commentRepo.GetCommentsByProductId(productId).ToList();

            foreach (var item in comments)
            {
              await _commentRepository.DeleteAsync(item.Id);
            }
        }

        public IQueryable<CommentEntity> GetAllComments()
        {
            return _commentRepository.GetAll();
        }

       

        public List<CommentListDto> GetComments()
        {
            var commentEntities = _commentRepository.GetAll().OrderByDescending(x => x.CreatedDate);
            var commentDtoList =commentEntities.Select(x => new CommentListDto()
            {

                Id = x.Id,
                CreatedDate = x.CreatedDate,
                ProductId = x.ProductId,
                UserId = x.UserId,
                Content = x.Content,
                Rating = x.Rating
               

            }).ToList();

            return commentDtoList;
        }

        public IQueryable<CommentEntity> GetCommentsByProduct(int productId)
        {
            return _commentRepo.GetCommentsByProductId(productId);
        }

        public  bool UserHasCommented(int productId, int userId)
        {
            return  _commentRepository.GetAll()
               .Any(pl => pl.ProductId == productId && pl.UserId == userId);
        }
    }
}
