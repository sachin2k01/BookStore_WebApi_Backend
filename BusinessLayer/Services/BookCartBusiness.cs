using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BookCartBusiness:IBookCartBusiness
    {
        private readonly IBookCartRepo _ibookCartRepo;
        public BookCartBusiness(IBookCartRepo ibookCartRepo)
        {
            _ibookCartRepo = ibookCartRepo;
        }
        public CartModel AddBookCart(CartModel cart, int userId)
        {
            return _ibookCartRepo.AddBookCart(cart, userId);
        }
    }
}
