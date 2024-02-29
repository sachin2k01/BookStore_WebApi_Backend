using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entities
{
    public class CartEntity
    {
        public int cartId { get; set; }
        public int userId { get; set; }
        public int bookId {  get; set; }
        public int quantit {  get; set; }
    }
}
