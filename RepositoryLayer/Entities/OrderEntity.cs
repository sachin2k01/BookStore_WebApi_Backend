﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entities
{
    public class OrderEntity
    {
        public int orderId {  get; set; }
        public int userId {  get; set; }
        public int bookId { get; set; }
        public int quantity {  get; set; }
    }
}
