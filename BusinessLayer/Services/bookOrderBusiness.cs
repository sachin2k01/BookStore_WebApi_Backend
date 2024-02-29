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
    public class bookOrderBusiness:IBookOrderBusiness
    {
        private readonly IBookOrderRepo _ibookOrderRepo;
        public bookOrderBusiness(IBookOrderRepo ibookOrderRepo)
        {
            _ibookOrderRepo = ibookOrderRepo;
        }
        public OrderModel OrderBook(OrderModel order, int userId)
        {
            return _ibookOrderRepo.OrderBook(order, userId);
        }
        public IEnumerable<OrderEntity> GetAllOrder(int userId)
        {
            return _ibookOrderRepo.GetAllOrder(userId);
        }
    }
}
