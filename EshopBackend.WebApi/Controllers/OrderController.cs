using AutoMapper;
using EshopBackend.Shared.Dtos.Order;
using EshopBackend.Shared.Interfaces;
using EshopBackend.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EshopBackend.WebApi.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IMapper mapper;

        public IOrderService OrderService { get; }

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            OrderService = orderService;
            this.mapper = mapper;
        }

        // GET: api/<OrderController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<OrderOutputDto>> GetOrder()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier);
            var order = await this.OrderService.GetOrder(Convert.ToInt64(userId.Value));
            return ApiResponse.Ok(mapper.Map<OrderOutputDto>(order));
        }

        // POST api/<OrderController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderOutputDto>> CreateOrder()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier);
            var order = await this.OrderService.CreateOrder(Convert.ToInt64(userId.Value));
            if (order == null)
                return ApiResponse.NotFound();
            return ApiResponse.Ok(mapper.Map<OrderOutputDto>(order));
        }

        // PUT api/<OrderController>/        
        [HttpPut()]
        public async Task<ActionResult<OrderOutputDto>> AddProductToOrder(OrderLineInputDto orderInput)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier);

            var order = await this.OrderService.AddProductToOrder(Convert.ToInt64(userId.Value), orderInput.ProductId, orderInput.OrderQty);
            return ApiResponse.Ok(mapper.Map<OrderOutputDto>(order));
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("delete-basket")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteOrder()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return ApiResponse.Ok(await this.OrderService.DeleteBasket(Convert.ToInt64(userId)));
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("delete-item/{productId}")]
        [Authorize]
        public async Task<ActionResult<OrderOutputDto>> DeleteOrder(long productId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = await this.OrderService.DeleteBasketItem(Convert.ToInt64(userId), productId);
            return ApiResponse.Ok(mapper.Map<OrderOutputDto>(res));
        }
    }
}
