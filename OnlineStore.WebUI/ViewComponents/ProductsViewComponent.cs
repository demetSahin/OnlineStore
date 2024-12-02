using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.Dtos;
using OnlineStore.WebUI.Models;


namespace OnlineStore.WebUI.ViewComponents
{
    public class ProductsViewComponent:ViewComponent
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsViewComponent(IProductService productService,IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }


        public IViewComponentResult Invoke(int? categoryId = null)
        {
            var productDtos = _productService.GetProductsByCategoryId(categoryId);

            var viewModel = productDtos.Select(x => new ProductListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                CategoryName = x.CategoryName,
                UnitsInStock = x.UnitsInStock,
                UnitPrice = x.UnitPrice,
                ImagePath = x.ImagePath,
                LikeCount = x.LikeCount,
                ProductLikeViewModels = _mapper.Map<List<ProductLikeViewModel>>(x.ProductLikeDtos),
                CommentViewModels = _mapper.Map<List<CommentViewModel>>(x.CommentDtos),
               
            }).ToList();

            return View(viewModel);
        }
    }
}
