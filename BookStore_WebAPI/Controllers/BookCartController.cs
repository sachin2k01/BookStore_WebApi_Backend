using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;

namespace BookStore_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCartController : ControllerBase
    {
        private readonly IBookCartBusiness _ibookCartBusiness;
        public BookCartController(IBookCartBusiness ibookCartBusiness)
        {
            _ibookCartBusiness = ibookCartBusiness;
        }

        [HttpPost]
        [Route("addTocart")]
        [Authorize]
        public IActionResult AddToCart(CartModel cartModel)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var cart = _ibookCartBusiness.AddBookCart(cartModel, userId);
            if(cart != null)
            {
                return Ok(cart);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
