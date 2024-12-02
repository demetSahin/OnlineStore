using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.ConcreteServices;
using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserOrdersList(int id)
        {
            var orderDtoList =  _orderService.GetUserOrders(id);

            var viewModel = orderDtoList.Select(x => new OrderListViewModel()
            {
                Id = x.Id,
                UserId = x.UserId,
                CreatedDate = x.CreatedDate,
                Fullname = x.Fullname,
                AddressName = x.AddressName,
                Address = x.Address,
                City = x.City,
                District = x.District,
                ZipCode = x.ZipCode,
                TotalPrice = x.TotalPrice

            }).ToList();

            return View(viewModel);
        }
    }
}
