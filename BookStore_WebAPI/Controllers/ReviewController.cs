using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;

namespace BookStore_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewBusiness _ireviewBusiness;
        public ReviewController(IReviewBusiness ireviewBusiness)
        {
            _ireviewBusiness = ireviewBusiness;
        }


        [HttpPost]
        [Route("addReview")]
        [Authorize]
        public IActionResult AddReview(ReviewModel revModel)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if(userId == 0)
            {
                return BadRequest();
            }
            var review = _ireviewBusiness.AddReview(revModel, userId);
            if(review != null)
            {
                return Ok(review);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
