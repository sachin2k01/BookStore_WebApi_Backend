using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ReviewBusiness:IReviewBusiness
    {
        private readonly IReviewRepo _ireviewRepo;
        public ReviewBusiness(IReviewRepo ireviewRepo)
        {
            _ireviewRepo = ireviewRepo;
        }
        public ReviewModel AddReview(ReviewModel revModel, int userId)
        {
            return _ireviewRepo.AddReview(revModel, userId);
        }
    }
}
