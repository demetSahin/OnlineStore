using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.Dtos;
using OnlineStore.WebUI.Models;


namespace OnlineStore.WebUI.ViewComponents
{
    public class ProductsDetailViewComponent : ViewComponent
    {
      
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsDetailViewComponent(IProductService productService,IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public IViewComponentResult Invoke(int id)
        {
            var productDto = _productService.GetProductDetail(id);

            var viewModel = new ProductDetailViewModel()
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                UnitPrice = productDto.UnitPrice,
                UnitsInStock = productDto.UnitsInStock,
                ImagePath = productDto.ImagePath,
                LikeCount = productDto.LikeCount,
                ProductLikeViewModels = _mapper.Map<List<ProductLikeViewModel>>(productDto.ProductLikeDtos),
                CommentViewModels = _mapper.Map<List<CommentViewModel>>(productDto.CommentDtos),
                AverageRating = productDto.AverageRating,
                TotalComments = productDto.TotalComments

            };
            return View(viewModel);
        }
    }
}
