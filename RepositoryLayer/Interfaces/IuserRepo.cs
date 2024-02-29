using ModelLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IuserRepo
    {
        public userRegisterModel RegisterUser(userRegisterModel model);
        public string LoginUser(userLoginModel loginModel);
        public string deleteUser(int userId);
        public List<userEntity> getAllUsers();

        public userEntity getUserDetails(int userId);
    }
}
