using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ModelLayer.Models;
using RepositoryLayer.Entities;
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

        public string UpdateAddress(AddressModel address,int adressId,int userId)
        {
            int row;
            addressEntity updatedAddress=new addressEntity();
            using(SqlConnection con=new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUserAddress", con);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@addressId", adressId);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@fullAddress", address.fullAddress);
                cmd.Parameters.AddWithValue("@city",address.city);
                cmd.Parameters.AddWithValue("@state", address.state);
                cmd.Parameters.AddWithValue("@typr", address.type);
                con.Open();
                row=cmd.ExecuteNonQuery();

            }
            if (row > 0)
            {
                return "update successfull";
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<addressEntity> GetAddress(int userId)
        {
            List<addressEntity> addressList = new List<addressEntity>();
            using(SqlConnection  connection=new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAddress", connection);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            addressEntity address = new addressEntity()
                            {
                                addressId = Convert.ToInt32(reader["adressId"]),
                                fullAddress = reader["fullAddress"].ToString(),
                                city = reader["city"].ToString(),
                                state = reader["state"].ToString(),
                                type = reader["type"].ToString(),
                                userId = Convert.ToInt32(reader["userId"])
                            };
                            addressList.Add(address);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions here, such as logging the error or throwing it further
                    Console.WriteLine("Error retrieving users: " + ex.Message);
                }
            }
            if(addressList!=null)
            {
                return addressList;
            }
            else
            {
                return null;
            }
        }
    }
}
