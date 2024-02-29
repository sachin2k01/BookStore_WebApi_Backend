using ModelLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IWishListBusiness
    {
        public string AddToWishList(wishListModel wishList, int userId);
        public wishListEntity RemoveBook(int WishListId);
    }
}
