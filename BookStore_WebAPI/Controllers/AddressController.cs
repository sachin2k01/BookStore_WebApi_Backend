using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;

namespace BookStore_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUserAddressBusiness _userAddressBusiness;
        public AddressController(IUserAddressBusiness userAddressBusiness)
        {
            _userAddressBusiness = userAddressBusiness;
        }

        [HttpPost]
        [Route("address")]
        [Authorize]
        public IActionResult addUserAddress(AddressModel address)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if(userId == 0)
            {
                return NotFound();
            }
            var userAddress=_userAddressBusiness.AddAdress(address, userId);
            if(userAddress == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(userAddress);
            }
        }
    }
}
