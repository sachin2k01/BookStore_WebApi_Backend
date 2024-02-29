using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IBookCartRepo
    {
        public CartModel AddBookCart(CartModel cart, int userId);
    }
}
