using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderservice;
        private readonly IMapper _mpaper;
        public OrdersController(IOrderService orderservice, IMapper mpaper)
        {
            _mpaper = mpaper;
            _orderservice = orderservice;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var address = _mpaper.Map<AddressDto, Core.Entities.OrderAggregate.Address>(orderDto.ShipToAddress);

            var order = await _orderservice.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if( order == null) return BadRequest(new ApiResponse(400, "Problem Creating Order"));

            return Ok(order);

        }

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
        {
           var email = HttpContext.User.RetrieveEmailFromPrincipal();

           var orders = await _orderservice.GetOrderForUserAsync(email);

           return Ok(_mpaper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var order = await _orderservice.GetOrderById(id, email);

            if(order == null) return NotFound(new ApiResponse(404));

            return _mpaper.Map<Order, OrderToReturnDto>(order);


        }

        [HttpGet("deliveryMethods")]

        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderservice.GetDeliveryMethodsAsync());
        }
    }
}