using ModelLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IBookOrderRepo
    {
        public OrderModel OrderBook(OrderModel order, int userId);
        public IEnumerable<OrderEntity> GetAllOrder(int userId);
    }
}
