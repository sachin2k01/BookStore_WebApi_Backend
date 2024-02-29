using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class reviewRepo:IReviewRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public reviewRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");

        }

        public ReviewModel AddReview(ReviewModel revModel,int userId)
        {
            using(SqlConnection con=new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddReview", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@review", revModel.review);
                cmd.Parameters.AddWithValue("@rating", revModel.rating);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@bookId", revModel.bookId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            if (revModel != null)
            {
                return revModel;
            }
            else
            {
                return null;
            }
        }
    }
}
