using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace managing_humanitarian_collections_api.Controllers
{

    [Route("/api/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public ActionResult CreateOrder([FromBody] CreateOrderDto dto)
        {

            var id = _orderService.CreateOrder(dto);

            return Created($"/api/order/{id}", null);
        }

        [HttpPost("{orderId}/orderProducts")]
        public ActionResult AddProductToOrder([FromRoute] int orderId, [FromBody] AddProductToOrderDto dto)
        {
            var newListId = _orderService.AddProductsToOrder(orderId, dto);

            return Created($"api/order/{orderId}/orderProducts/{newListId}", null);

        }

        [HttpPut("{id}")]
        public ActionResult UpdateStatus([FromBody] UpdateOrderStatusDto dto, [FromRoute] int id)
        {

            _orderService.UpdateOrderStatus(id, dto);

            return Ok();
        }
        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetAll()
        {
            var orderDtos = _orderService.GetAll();

            return Ok(orderDtos);
        }
        [HttpGet("ordersPerDonator/{id}")]
        public ActionResult GetDonatorOrders([FromRoute] int id)
        {
            var orders = _orderService.GetOrdersPerDonator(id);
            return Ok(orders);
        }

    }
}
