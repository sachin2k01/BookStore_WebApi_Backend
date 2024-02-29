using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class wishListBusiness:IWishListBusiness
    {
        private readonly IWishListRepo _iwishListRepo;
        public wishListBusiness(IWishListRepo iwishListRepo)
        {
            _iwishListRepo = iwishListRepo;
        }
        public string AddToWishList(wishListModel wishList, int userId)
        {
            return _iwishListRepo.AddToWishList(wishList, userId);
        }
        public wishListEntity RemoveBook(int WishListId)
        {
            return _iwishListRepo.RemoveBook(WishListId);
        }

    }
}
