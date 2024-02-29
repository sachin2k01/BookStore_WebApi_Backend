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
    public class bookRepo:IBookRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public bookRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");

        }

        public BookModel AddBooks(BookModel book)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddBook", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@title", book.title);
                cmd.Parameters.AddWithValue("@author", book.author);
                cmd.Parameters.AddWithValue("@detail", book.detail);
                cmd.Parameters.AddWithValue("@image", book.image);
                cmd.Parameters.AddWithValue("@price", book.price);
                cmd.Parameters.AddWithValue("@quantity", book.quantity);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            if(book != null)
            {
                return book;
            }
            else
            {
                return null;
            }
        }

        public List<bookEntity> AllBook()
        {
            List<bookEntity> bookDetails = new List<bookEntity>();
            using (SqlConnection  conn = new SqlConnection(connectionString))
            {             
                SqlCommand cmd = new SqlCommand("spGetAllBook", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bookEntity book = new bookEntity()
                            {
                                bookId= Convert.ToInt32(reader["bookId"]),
                                title = reader["title"].ToString(),
                                author = reader["author"].ToString(),
                                detail = reader["detail"].ToString(),
                                image = reader["image"].ToString(),
                                price = Convert.ToDecimal(reader["price"]),
                                quantity = Convert.ToInt32(reader["quantity"])

                            };
                            bookDetails.Add(book);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions here, such as logging the error or throwing it further
                    Console.WriteLine("Error retrieving users: " + ex.Message);
                }
            }
            if (bookDetails != null)
            {
                return bookDetails;
            }
            else
            {
                return null;
            }
        }

        public bookEntity GetBook(int bookId)
        {
            bookEntity bookDetails=new bookEntity();
            using(SqlConnection con=new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetBookById", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookId", bookId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bookEntity book = new bookEntity()
                        {
                            bookId = Convert.ToInt32(reader["bookId"]),
                            title = reader["title"].ToString(),
                            author = reader["author"].ToString(),
                            detail = reader["detail"].ToString(),
                            image = reader["image"].ToString(),
                            price = Convert.ToDecimal(reader["price"]),
                            quantity = Convert.ToInt32(reader["quantity"])

                        };
                        bookDetails=book;
                    }
                }
                if(bookDetails != null) { return bookDetails;}
                else
                {
                    return null;
                }

            }
        }

    }
}
