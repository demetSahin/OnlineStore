using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.Dtos;
using OnlineStore.DAL.Entities;

using OnlineStore.WebUI.Extensions;
using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.Controllers
{
    public class ShoppingBasketController : Controller
    {
        private readonly IShoppingBasketService _shoppingBasketService;
        
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public ShoppingBasketController(IShoppingBasketService shoppingBasketService, IMapper mapper, IOrderService orderService,IProductService productService)
        {
            _shoppingBasketService = shoppingBasketService;
            _mapper = mapper;
            _orderService = orderService;
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddToBasket(int userId, int productId)
        {
            ShoppingBasketViewModel shoppingBasketViewModel = new ShoppingBasketViewModel();
            shoppingBasketViewModel.UserId = userId;
            shoppingBasketViewModel.ProductId = productId;
            _shoppingBasketService.AddToBasket(_mapper.Map<ShoppingBasketDto>(shoppingBasketViewModel));

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> AddToBasket()
        {
            var shoppingBasket = await _shoppingBasketService.GetShoppingBasket();
            return View(_mapper.Map<List<ShoppingBasketViewModel>>(shoppingBasket));
        }
       
        public async Task<IActionResult> DeleteFromBasket(int id)
        {
             await _shoppingBasketService.DeleteFromBasket(id);

            return RedirectToAction("AddToBasket","ShoppingBasket");
        }

        public async Task<IActionResult> BasketTotal()
        {
           var total =  await _shoppingBasketService.GetTotalPrice();
            return View(total);

        }
        [HttpGet]
        public async Task <IActionResult> CheckOut()
        {
            return View(new ShippingDetailsViewModel());
        }
        [HttpPost]

      
        public async Task<IActionResult> CheckOut(ShippingDetailsViewModel shippingDetails)
        {
            var basket = await _shoppingBasketService.GetShoppingBasket();

            if (basket == null || !basket.Any())
            {
                ViewBag.ErrorMessage = "Sepette Ürün Bulunmamaktadır..";
                return View(shippingDetails); // Sepet boşsa görünümü döndür
            }

            if (ModelState.IsValid)
            {
                var orderViewModel = new OrderViewModel
                {
                    CreatedDate = DateTime.Now,
                    UserId = User.GetId(),
                    City = shippingDetails.City,
                    District = shippingDetails.District,
                    Address = shippingDetails.Address,
                    AddressName = shippingDetails.AddressName,
                    ZipCode = shippingDetails.ZipCode,
                   // Price = await _shoppingBasketService.GetTotalPrice(),
                   
                    Fullname = shippingDetails.Fullname
                    
                };
                List<OrderDetailViewModel> viewModels = new List<OrderDetailViewModel>();
                
                decimal? totalPrice = 0;
                foreach (var item in basket)
                {
                    var productPrice = _productService.GetProductDetail(item.ProductId).UnitPrice;
                    totalPrice += productPrice;
                }
                orderViewModel.TotalPrice = totalPrice;
                var orderDto = _mapper.Map<OrderAddDto>(orderViewModel);
                await _shoppingBasketService.SaveOrder(orderDto);
                var order = _orderService.GetOrder();
                foreach (var item in basket)
                {
                    var orderDetailViewModel = new OrderDetailViewModel();
                    orderDetailViewModel.ProductId = item.ProductId;
                    var productPrice = _productService.GetProductDetail(item.ProductId).UnitPrice;
                    orderDetailViewModel.OrderId = order.Id;
                    orderDetailViewModel.Price = productPrice;
                    await _shoppingBasketService.AddOrderDetail(_mapper.Map<OrderDetailDto>(orderDetailViewModel));

                }
                foreach (var item in basket)
                {
                   await _shoppingBasketService.DeleteFromBasket(item.Id);
                }
                return View("Completed", "ShoppingBasket");
            }
            else
            {
                return View(shippingDetails); // Model geçerli değilse görünümü döndür
            }
        }





















        //public async Task<IActionResult> CheckOut(ShippingDetailsViewModel shippingDetails)
        //{
        //    var basket = await _shoppingBasketService.GetShoppingBasket();


        //    if (basket.Count == 0)
        //    {
        //        ViewBag.ErrorMessage = "Sepette Ürün Bulunmamaktadır..";
        //    }


        //    if (ModelState.IsValid)
        //    {


        //        var orderViewModel = new OrderViewModel();
        //            orderViewModel.CreatedDate = DateTime.Now;
        //            orderViewModel.UserId = User.GetId();
        //            orderViewModel.City = shippingDetails.City;
        //            orderViewModel.District = shippingDetails.District;
        //            orderViewModel.Address = shippingDetails.Address;
        //            orderViewModel.AddressName = shippingDetails.AddressName;
        //            orderViewModel.ZipCode = shippingDetails.ZipCode;
        //            orderViewModel.OrderDetailsViewModels = new List<OrderDetailViewModel>();
        //            orderViewModel.Price = await _shoppingBasketService.GetTotalPrice();



        //            foreach (var item in basket)
        //            {
        //                var orderDetail = new OrderDetailViewModel();

        //                orderDetail.ProductId = item.ProductId;
        //                orderDetail.ProductViewModel = item.Product;
        //                orderDetail.Price = (decimal)item.ProductViewModel.UnitPrice;


        //                orderViewModel.OrderDetailsViewModels.Add(orderDetail);
        //                var orderDet = _mapper.Map<OrderDetailDto>(orderDetail);
        //                await _shoppingBasketService.AddOrderDetail(orderDet);

        //            }

        //            var orderDto = _mapper.Map<OrderAddDto>(orderViewModel);
        //           await _shoppingBasketService.AddOrder(orderDto);

        //        //siparişi kaydetme
        //        await _shoppingBasketService.SaveOrder();
        //        return View("Completed", "ShoppingBasket");

        //    }
        //    else
        //    {
        //        // var modelDto = _mapper.Map<ShippingDetailsDto>(model);
        //        return View(shippingDetails);
        //    }



        //}
        //public async void SaveOrder(List<ShoppingBasketViewModel> basket, ShippingDetailsViewModel shippingDetails)
        //{
        //    var orderViewModel = new OrderViewModel();
        //    orderViewModel.CreatedDate = DateTime.Now;
        //    orderViewModel.UserId = User.GetId();
        //    orderViewModel.City = shippingDetails.City;
        //    orderViewModel.District = shippingDetails.District;
        //    orderViewModel.Address = shippingDetails.Address;
        //    orderViewModel.AddressName = shippingDetails.AddressName;
        //    orderViewModel.ZipCode = shippingDetails.ZipCode;
        //    orderViewModel.OrderDetailsViewModels = new List<OrderDetailViewModel>();
        //    orderViewModel.Price = await _shoppingBasketService.GetTotalPrice();
        //    foreach (var item in basket)
        //    {
        //        var orderDetail = new OrderDetailViewModel();

        //        orderDetail.ProductId = item.ProductId;
        //        orderDetail.ProductViewModel = item.ProductViewModel;
        //        orderDetail.Price = (decimal)item.ProductViewModel.UnitPrice;


        //        orderViewModel.OrderDetailsViewModels.Add(orderDetail);
        //        var orderDet = _mapper.Map<OrderDetailDto>(orderDetail);
        //        await _shoppingBasketService.AddOrderDetail(orderDet);

        //    }

        //    var orderDto = _mapper.Map<OrderAddDto>(orderViewModel);
        //   await _shoppingBasketService.AddOrder(orderDto);

        //}

    }
}
