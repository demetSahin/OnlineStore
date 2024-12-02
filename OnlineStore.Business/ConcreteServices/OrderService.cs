using AutoMapper;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.Dtos;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.ConcreteServices
{
   
    
    public class OrderService : IOrderService
    {
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IOrderRepository _orderRepo;
        private readonly IRepository<OrderDetailEntity> _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<OrderEntity> orderRepository, IMapper mapper, IOrderRepository orderRepo,IRepository<OrderDetailEntity> orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderRepo = orderRepo;
            _orderDetailRepository = orderDetailRepository;
        }

        public List<OrderListDto> GetAllOrders()
        {
            var orderEntities = _orderRepository.GetAll().OrderByDescending(x => x.CreatedDate);
            var orderDtoList = orderEntities.Select(x => new OrderListDto()
            {

                Id = x.Id,
                CreatedDate = x.CreatedDate,
                Fullname = x.Fullname,
                AddressName = x.AddressName,
                Address = x.Address,
                City = x.City,
                District = x.District,
                ZipCode = x.ZipCode,
                TotalPrice = x.TotalPrice

            }).ToList();

            return orderDtoList;
        }

        public OrderDto GetOrder()
        {
            var orderEntity = _orderRepository.GetAll().OrderByDescending(x => x.CreatedDate).FirstOrDefault();

            return _mapper.Map<OrderDto>(orderEntity);

        }

        public List<OrderListDto> GetUserOrders(int? userId = null)
        {

            var orderEntities = _orderRepository.GetAll(x => x.UserId == userId).OrderByDescending(x => x.CreatedDate);

            var orderDtoList = orderEntities.Select(x => new OrderListDto()
            {

                Id = x.Id,
                CreatedDate = x.CreatedDate,
                Fullname = x.Fullname,
                AddressName = x.AddressName,
                Address = x.Address,
                City = x.City,
                District = x.District,
                ZipCode = x.ZipCode,
                TotalPrice = x.TotalPrice

            }).ToList();

            return orderDtoList;
        }



        public async Task<OrderUpdateDto> GetOrderById(int id)
        {
            var entity = await _orderRepository.GetByIdAsync(id);
            var orderUpdateDto = new OrderUpdateDto()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                CreatedDate = entity.CreatedDate,
                Fullname = entity.Fullname,
                AddressName = entity.AddressName,
                Address = entity.Address,
                City = entity.City,
                District = entity.District,
                ZipCode = entity.ZipCode,
                TotalPrice = entity.TotalPrice

            };
            return orderUpdateDto;
        }

        public async Task Update(OrderUpdateDto orderUpdateDto)
        {
            var order = await _orderRepository.GetByIdAsync(orderUpdateDto.Id);
            order.Id = orderUpdateDto.Id;
            order.CreatedDate = orderUpdateDto.CreatedDate;
            order.Fullname = orderUpdateDto.Fullname;
            order.AddressName = orderUpdateDto.AddressName;
            order.Address = orderUpdateDto.Address;
            order.City = orderUpdateDto.City;
            order.District = orderUpdateDto.District;
            order.ZipCode = orderUpdateDto.ZipCode;
            order.UserId = orderUpdateDto.UserId;
            order.TotalPrice = orderUpdateDto.TotalPrice;

            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrder(int id)
        {
          await _orderRepo.DeleteOrderWithDetails(id); 
        }
    }
}
