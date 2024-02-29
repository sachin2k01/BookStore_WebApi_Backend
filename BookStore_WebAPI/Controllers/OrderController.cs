using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entities;

namespace BookStore_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IBookOrderBusiness _ibookOrderBusiness;
        public OrderController(IBookOrderBusiness ibookOrderBusiness)
        {
            _ibookOrderBusiness = ibookOrderBusiness;
        }

        [HttpPost]
        [Route("order")]
        [Authorize]
        public IActionResult AddBookOrder(OrderModel order)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if(userId == 0)
            {
                return NotFound();
            }
            var orderDetails=_ibookOrderBusiness.OrderBook(order, userId);
            if(orderDetails == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(orderDetails);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getOrders")]
        public IActionResult GetAllOrders()
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if (userId == 0)
            {
                return NotFound();
            }

            IEnumerable<OrderEntity> orders = _ibookOrderBusiness.GetAllOrder(userId);
            if(orders!=null)
            {
                return Ok(orders);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}