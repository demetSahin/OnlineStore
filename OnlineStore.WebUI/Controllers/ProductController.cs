using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.ConcreteServices;
using OnlineStore.Business.Dtos;
using OnlineStore.Business.Extensions;
using OnlineStore.WebUI.Models;



namespace OnlineStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        // private readonly ILikeService _likeService;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LikeProduct(int productId, int userId)
        {
            await _productService.LikeProduct(productId, userId);
            TempData["ProductId"] = productId;
            return View("Detail", "Product");
        }

        public async Task<IActionResult> Detail(int id)
        {
            TempData["ProductId"] = id;
            return View();


        }

        [HttpPost]
        public async Task<IActionResult> SearchTerm(string searchedTerm)
        {
            var productViewModels = await _productService.Search(searchedTerm);
            return RedirectToAction("Index", "Home", _mapper.Map<List<ProductViewModel>>(productViewModels));
        }
        [HttpGet]
        public async Task<IActionResult> CommentProduct(int productId)
        {
          
            var model = new CommentViewModel
            {
                ProductId = productId // ProductId ViewModel'e atanıyor

            };


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CommentProduct(CommentViewModel model)
        {

           
            var addDto = new CommentDto()
            {
                ProductId = model.ProductId,
                UserId = User.GetId(),
                Content = model.Content,
                Rating = model.Rating,
                ProductDto = _mapper.Map<ProductDto>(model.ProductViewModel),
                UserDto = _mapper.Map<UserDto>(model.UserViewModel)

            };


            await _productService.CommentProduct(addDto.ProductId, addDto.UserId, addDto.Content, addDto.Rating);
            TempData["ProductId"] = model.ProductId;
            return View("Detail", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int productId)
        {

            int userId = User.GetId();
            if (productId <= 0 || userId <= 0)
            {
                throw new Exception("Invalid product or user ID.");
            }
            await _productService.DeleteProductComment(productId, userId);
            TempData["ProductId"] = productId;
            return View("Detail", "Product");


        }
    }
}

