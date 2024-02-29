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
    public class userAddressRepo :IUserAddressRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public userAddressRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }


        public AddressModel AddAdress(AddressModel address,int userId)
        {
            using(SqlConnection connection= new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddUserAddress", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fullAddress", address.fullAddress);
                cmd.Parameters.AddWithValue("@city", address.city);
                cmd.Parameters.AddWithValue("@state", address.state);
                cmd.Parameters.AddWithValue("@type", address.type);
                cmd.Parameters.AddWithValue("@userId", userId);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            return address;
        }
    }
}
