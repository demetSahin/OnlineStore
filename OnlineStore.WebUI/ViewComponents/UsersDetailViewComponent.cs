using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Business.AbstractServices;
using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.ViewComponents
{
    public class UsersDetailViewComponent:ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILikeService _likeService;

        public UsersDetailViewComponent(IUserService userService, IMapper mapper, ILikeService likeService)
        {
            _userService = userService;
            _mapper = mapper;
            _likeService = likeService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var userDto = _userService.GetUserDetail(id);
            // Kullanıcının favori ürünlerini al
            var likedProducts = await _likeService.GetUserLikedProductsAsync(id);

            var viewModel = new UserDetailViewModel()
            {
                Id = userDto.Id,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                LikedProducts = likedProducts.Select(x => new UserLikedProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    UnitPrice =(decimal) x.UnitPrice,
                    ImagePath = x.ImagePath,
                    UnitsInStock = x.UnitsInStock
                }).ToList()

            };
            ViewData["UserId"] = id;  // User ID'yi ViewData'ya ekleyerek favori ürünler sayfasına yönlendirme 
            return View(viewModel);
        }
    }
}
