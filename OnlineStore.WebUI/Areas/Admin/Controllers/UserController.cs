using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Business.AbstractServices;
using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService)
        {
            _userService = userService;
            
        }
        public async Task<IActionResult> List()
        {
            var userDtoList = await _userService.GetAllUsers();

            var viewModel = userDtoList.Select(x => new UserListViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                CreatedDate = x.CreatedDate,
                
            }).ToList();

            return View(viewModel);
        }
    }
}
