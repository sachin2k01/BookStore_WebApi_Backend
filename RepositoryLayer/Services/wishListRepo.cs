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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RepositoryLayer.Services
{
    public class wishListRepo:IWishListRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public wishListRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");

        }

        public string AddToWishList(wishListModel wishList,int userId)
        {
            using(SqlConnection  conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddWishList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookId", wishList.bookId);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            if(wishList!=null)
            {
                return "books added successfully";
            }
            else
            {
                return "invalid Details";
            }
        }


        public wishListEntity RemoveBook(int WishListId)
        {
            using(SqlConnection  con = new SqlConnection(connectionString))
            {
                wishListEntity book=new wishListEntity();
                SqlCommand cmd = new SqlCommand("spRemoveWishListBook", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@wishListId", WishListId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        wishListEntity wishBook = new wishListEntity()
                        {
                            userId = Convert.ToInt32(reader["userId"]),
                            bookId= Convert.ToInt32(reader["booklId"]),
                            wishList = Convert.ToInt32(reader["wishList"])


                        };
                        book = wishBook;
                    }
                }
                if(book!=null)
                {
                    return book;
                }
                else
                {
                    return null;
                }
            }

        }
    }
}
