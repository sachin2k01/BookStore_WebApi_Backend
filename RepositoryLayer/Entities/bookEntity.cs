using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entities
{
    public class bookEntity
    {
        public int bookId {  get; set; }
        public string title { get; set; }
        public string author {  get; set; }
        public string detail {  get; set; }
        public string image {  get; set; }
        public decimal price { get; set; }  
        public int quantity {  get; set; }

    }
}
