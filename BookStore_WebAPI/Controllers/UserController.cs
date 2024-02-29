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
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _iuserBusiness;
        public UserController( IUserBusiness iuserBusiness)
        {
            _iuserBusiness = iuserBusiness;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult userRegister(userRegisterModel userModel)
        {
            var registerUser=_iuserBusiness.RegisterUser(userModel);
            if (registerUser != null)
            {
                return Ok(new ResponseModel<userRegisterModel> { Success = true, Message = "Register Successfull", Data = registerUser });
            }
            else
            {
                return BadRequest(new ResponseModel<userRegisterModel> { Success = false, Message = "User Register Failed" });
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult userLogin(userLoginModel userLogin)
        {
            var loginUser = _iuserBusiness.LoginUser(userLogin);
            if(loginUser != null)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "Login Successfull", Data = loginUser });
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("deleteUser")]
        public IActionResult deleteUser(int userId)
        {
            var delUser=_iuserBusiness.deleteUser(userId);
            if(delUser != null)
            {
                return Ok(new ResponseModel<string> { Success = true, Data = delUser });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Data = delUser });
            }
        }

        [HttpGet]
        [Route("allUser")]
        public IActionResult getAllUsers()
        {
            List<userEntity> userslist = new List<userEntity>();
            userslist = _iuserBusiness.getAllUsers();
            if(userslist != null)
            {
                return Ok(userslist);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("userDetail")]
        [Authorize]
        public IActionResult getUserDetails()
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if(userId == 0)
            {
                return NotFound();
            }
            else
            {
                userEntity user=_iuserBusiness.getUserDetails(userId);
                if (user != null)
                    return Ok(new ResponseModel<userEntity> { Success = true, Message = "Login Successfull", Data = user });
                return BadRequest();
            }

        }
    }
}
