using AutoMapper;
using Order.Application.Interfaces;
using Order.Application.Interfaces.Services;
using Order.Domain.Entities;
using Order.Domain.Models;

namespace Order.Application.Features
{
    public class CartService : ICartService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public CartService(IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<CartDetailsViewModel> GetCartDetails(string customerId)
        {
            await _orderRepository.EnsureCartExists(customerId);

            return _mapper.Map<Domain.Entities.Order,CartDetailsViewModel>(await _orderRepository.GetCartDetails(customerId));
        }

        public async Task<CartDetailsViewModel> AddCartPromocode(string customerId, string promocode)
        {
            await _orderRepository.EnsureCartExists(customerId);

            return _mapper.Map<Domain.Entities.Order, CartDetailsViewModel>(await _orderRepository.SetCartPromocode(customerId, promocode));
        }

        public async Task<CartItemViewModel> AddCartItem(string customerId, CartItemViewModel cartItem)
        {
            OrderItem orderItemEntity = _mapper.Map<CartItemViewModel, OrderItem>(cartItem);

            await _orderRepository.EnsureCartExists(customerId);

            orderItemEntity.CartCustomerId = customerId;

            await _orderRepository.AddCartItem(orderItemEntity);

            return _mapper.Map<OrderItem,CartItemViewModel>(orderItemEntity);
        }


    }
}

