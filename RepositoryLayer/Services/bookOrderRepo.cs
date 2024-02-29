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
    public class bookOrderRepo:IBookOrderRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public bookOrderRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public OrderModel OrderBook(OrderModel order,int userId)
        {
            using(SqlConnection connection= new SqlConnection(connectionString)) 
            {
                SqlCommand cmd = new SqlCommand("spOrderBook", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookId", order.bookId);
                cmd.Parameters.AddWithValue("@quantity", order.quantity);
                cmd.Parameters.AddWithValue("@userId", userId);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            if(order == null)
            {
                return null;
            }
            else
            {
                return order;
            }
        }

        public IEnumerable<OrderEntity> GetAllOrder(int userId)
        {
            List<OrderEntity> orders = new List<OrderEntity>();
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllOrders", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderEntity order = new OrderEntity()
                            {
                                orderId = Convert.ToInt32(reader["orderId"]),
                                userId = Convert.ToInt32(reader["userId"]),
                                bookId = Convert.ToInt32(reader["bookId"]),
                                quantity = Convert.ToInt32(reader["quantity"])
                            };
                            orders.Add(order);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions here, such as logging the error or throwing it further
                    Console.WriteLine("Error retrieving users: " + ex.Message);
                }
            }
            if(orders!=null)
            {
                return orders;
            }
            else
            {
                return null;
            }
        }

        
    }
}
