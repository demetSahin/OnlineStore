using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.Dtos;
using OnlineStore.Business.Extensions;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace OnlineStore.Business.ConcreteServices
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly IRepository<ShoppingBasket> _shoppingRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderDetailEntity> _orderDetailRepository;

        public ShoppingBasketService(IRepository<ShoppingBasket> shoppingRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRepository<OrderEntity> orderRepository,IRepository<OrderDetailEntity> orderDetailRepository)
        {
            _shoppingRepository = shoppingRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task AddOrder(OrderAddDto orderAddDto)
        {
            await _orderRepository.AddAsync(_mapper.Map<OrderEntity>(orderAddDto));
        }

        public async Task AddOrderDetail(OrderDetailDto orderDetailDto)
        {
            await _orderDetailRepository.AddAsync(_mapper.Map<OrderDetailEntity>(orderDetailDto));
        }

        public async Task AddToBasket(ShoppingBasketDto shoppingBasketDto)
        {
            await _shoppingRepository.AddAsync(_mapper.Map<ShoppingBasket>(shoppingBasketDto));
        }

        public async Task DeleteFromBasket(int id)
        {
            await _shoppingRepository.DeleteAsync(id);
        }

        public async Task<List<ShoppingBasketDto>> GetShoppingBasket()
        {
            var shoppingBasket = await  _shoppingRepository.GetAllAsync();

            return _mapper.Map<List<ShoppingBasketDto>>(shoppingBasket);
        }

        public async Task<decimal> GetTotalPrice()
        {
            decimal totalPrice = 0;

            // Verileri listeye dönüştürüyoruz, böylece veritabanına hemen sorgu yapılır
            var shoppingBasket = await _shoppingRepository.GetAll().ToListAsync();

            foreach (var item in shoppingBasket)
            {
                if (item.Product != null)
                {
                    totalPrice += (decimal)item.Product.UnitPrice;
                }
            }

            return totalPrice;
        }

        

        public async Task<bool> SaveOrder(OrderAddDto orderDto)
        {
     


            try
            {
                await _orderRepository.AddAsync (_mapper.Map<OrderEntity> (orderDto)); // Sadece ekleme işlemi yapılır
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
