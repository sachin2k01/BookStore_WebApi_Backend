using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace BookStore_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListRepo _iwishList;
        public WishListController(IWishListRepo wishListRepo)
        {
            _iwishList = wishListRepo;
        }

        [HttpPost]
        [Route("wishlist")]
        [Authorize]
        public IActionResult AddBookWishList(wishListModel model)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if (userId == 0)
            {
                return NotFound();
            }
            var wishlist=_iwishList.AddToWishList(model,userId);
            if(wishlist == null)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "failed", Data = wishlist });
            }
            else
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "sucess", Data = wishlist });
            }
        }

        [HttpDelete]
        [Route("removeWish")]
        [Authorize]
        public IActionResult RemoveWishListBook(int wishListId)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if(userId == 0)
            {
                return BadRequest();
            }
            var book=_iwishList.RemoveBook(wishListId);
            if(book == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(new ResponseModel<wishListEntity> { Success = true, Message = "removed from wishList", Data = book });
            }

        }
    }
}
