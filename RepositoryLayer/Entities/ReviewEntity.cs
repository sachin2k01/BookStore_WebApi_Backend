using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entities
{
    public class ReviewEntity
    {
        public int reviewId { get; set; }
        public string review {  get; set; }
        public int rating {  get; set; }
        public int bookId {  get; set; }
        public int userId {  get; set; }
    }
}
