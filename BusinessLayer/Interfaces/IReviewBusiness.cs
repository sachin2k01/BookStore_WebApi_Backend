using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IReviewBusiness
    {
        public ReviewModel AddReview(ReviewModel revModel, int userId);
    }
}
