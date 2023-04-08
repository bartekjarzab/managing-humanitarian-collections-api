using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
using managing_humanitarian_collections_api.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using managing_humanitarian_collections_api.Models.Order;

namespace managing_humanitarian_collections_api.Controllers
{

    [Route("/api")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        #region Tworzenie zamowienia
        [HttpPost("collection/{collectionId}/orders")]
        public ActionResult CreateOrder([FromBody] CreateOrderDto dto, [FromRoute] int collectionId)
        {
            var newOrderId = _orderService.CreateOrder(collectionId, dto);


            return Ok(newOrderId);
            
        }
        #endregion
        #region Dodanie produktu do zamówienia
        [HttpPost("orders/{orderId}/orderProducts")]
        public ActionResult AddProductToOrder([FromRoute] int orderId, [FromBody] AddProductToOrderDto dto)
        {
            var newProductId = _orderService.AddProductsToOrder(orderId, dto);

            return Ok(newProductId);
        }
        #endregion
        #region Wszystkie zamówienia darczyńcy
        [HttpGet("users/{userId}/orders")]
        public ActionResult GetOrdersPerDonator([FromRoute] int userId)
        {
            var ordersDto = _orderService.GetAllDonatorOrders(userId);
            return Ok(ordersDto);
        }
        #endregion
        #region Wszystkie zamówienia zbiórki
        [HttpGet("collection/{collectionId}/orders")]
        public ActionResult GetOrdersPerCollection([FromRoute] int collectionId)
        {
            var ordersDto = _orderService.GetAllCollectionOrders(collectionId);
            return Ok(ordersDto);
        }
        #endregion
        [HttpPut("order/{id}")]
        public ActionResult UpdateStatus([FromBody] UpdateOrderStatusDto dto, [FromRoute] int id)
        {
            _orderService.UpdateOrderStatus(id, dto);

            return Ok("Status zamówienia został zmieniony");
        }
        
        [HttpGet("ordersPerDonator/{id}")]
        public ActionResult GetDonatorOrders([FromRoute] int id)
        {
            var orders = _orderService.GetOrdersPerDonator(id);
            return Ok(orders);
        }

        [HttpGet("orders/{orderId}/orderProducts")]

        public ActionResult GetOrderProductsPerOrder([FromRoute] int orderId)
        {
            var orderProducts = _orderService.GetProductsPerOrder(orderId);
            return Ok(orderProducts);
        }
        [HttpDelete("/order/{orderId}/orderProducts/{orderProductId}")]
        public ActionResult DeleteProductFromOrder([FromRoute] int orderId, int orderProductId)
        {
           _orderService.DeleteProductFromOrder(orderId, orderProductId);
            return Ok("Przedmiot usunięty z koszyka");
        }

    }
}
