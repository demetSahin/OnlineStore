using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.ConcreteServices;
using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILikeService _likeService;

        public UserController(IUserService userService, IMapper mapper, ILikeService likeService)
        {
            _userService = userService;
            _mapper = mapper;
            _likeService = likeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            ViewBag.UserId = id;
            return View();

        }
        public async Task<IActionResult> UserFavorites(int userId)
        {
            // Kullanıcının favori ürünlerini al
            var likedProducts = await _likeService.GetUserLikedProductsAsync(userId);

            var viewModel = likedProducts.Select(x => new UserLikedProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice =(decimal) x.UnitPrice,
                ImagePath = x.ImagePath,
                UnitsInStock = x.UnitsInStock

            }).ToList();

            return View(viewModel);
        }

    }
}
