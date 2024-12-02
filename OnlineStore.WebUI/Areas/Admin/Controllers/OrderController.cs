using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.Dtos;
using OnlineStore.WebUI.Areas.Admin.Models;

namespace OnlineStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] //program cs'teki area:exists ile eşleşir.
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult List()
        {
            var orderDtoList = _orderService.GetAllOrders();
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

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updateOrderDto = await _orderService.GetOrderById(id);
            var viewModel = new OrderUpdateViewModel()
            {
                Id = updateOrderDto.Id,
                UserId = updateOrderDto.UserId,
                CreatedDate = updateOrderDto.CreatedDate,
                Fullname = updateOrderDto.Fullname,
                AddressName = updateOrderDto.AddressName,
                Address = updateOrderDto.Address,
                City = updateOrderDto.City,
                District = updateOrderDto.District,
                ZipCode = updateOrderDto.ZipCode,
                TotalPrice = updateOrderDto.TotalPrice

            };


            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int orderId, OrderUpdateViewModel viewModel)
        {
            viewModel.Id = orderId;
            var orderUpdateDto = new OrderUpdateDto()
            {
                Id = viewModel.Id,
                UserId = viewModel.UserId,
                CreatedDate = viewModel.CreatedDate,
                Fullname = viewModel.Fullname,
                AddressName = viewModel.AddressName,
                Address = viewModel.Address,
                City = viewModel.City,
                District = viewModel.District,
                ZipCode = viewModel.ZipCode,
                TotalPrice = viewModel.TotalPrice

            };
            await _orderService.Update(orderUpdateDto);
            return RedirectToAction("List");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int orderId)
        {
            await _orderService.DeleteOrder(orderId);
            return RedirectToAction("List");


        }



    }
}
