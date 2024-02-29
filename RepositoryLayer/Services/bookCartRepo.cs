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
    public class bookCartRepo:IBookCartRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public bookCartRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");

        }

        public CartModel AddBookCart(CartModel cart,int userId)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddToCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@bookId", cart.bookId);
                cmd.Parameters.AddWithValue("@quantity", cart.quantit);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            if(cart == null)
            {
                return null;
            }
            else
            {
                return cart;
            }
        }
    }
}
