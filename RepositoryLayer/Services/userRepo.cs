using ModelLayer.Models;
using RepositoryLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using RepositoryLayer.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RepositoryLayer.Services
{
    public class userRepo : IuserRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public userRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }


        public userRegisterModel RegisterUser(userRegisterModel  model)
        {
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("spRegisterUser", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                connection.Open();
                sqlCommand.Parameters.AddWithValue("@userName", model.userName);
                sqlCommand.Parameters.AddWithValue("@email", model.email);
                sqlCommand.Parameters.AddWithValue("@password", EncryptPassword(model.password));
                sqlCommand.Parameters.AddWithValue("@mobnum", long.Parse(model.mobnum));
                sqlCommand.ExecuteNonQuery();
            }
            return model;
        }

        public static string EncryptPassword(string password)
        {
            try
            {
                byte[] encrypt_password = new byte[password.Length];
                encrypt_password = Encoding.UTF8.GetBytes(password);
                string encodedPassword = Convert.ToBase64String(encrypt_password);
                return encodedPassword;

            }
            catch (Exception ex)
            {
                return $"Encryption Failed.! {ex.Message}";
            }
        }

        public string LoginUser(userLoginModel loginModel)
        {
            userEntity user=new userEntity();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("spLoginUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@email", loginModel.email);
                command.Parameters.AddWithValue("@password", EncryptPassword(loginModel.password));
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userEntity userData = new userEntity()
                        {
                            userId = Convert.ToInt32(reader["userID"]),
                            userName = reader["userName"].ToString(),
                            email = reader["email"].ToString(),
                            password = reader["password"].ToString(),
                            mobnum = reader["mobnum"].ToString()

                        };
                            user = userData;                        
                    }
                }
            }
            if (user.email == loginModel.email && DecryptPassword(user.password) == loginModel.password)
            {
                var token = GenerateToken(user.email, user.userId);
                return token;
            }
            else
            {
                return null;
            }
        }


        public static string DecryptPassword(string encryptedPassword)
        {
            try
            {
                byte[] decrypt_password = Convert.FromBase64String(encryptedPassword);
                string originalPassword = Encoding.UTF8.GetString(decrypt_password);
                return originalPassword;
            }
            catch (Exception ex)
            {
                return $"Decryption Failed.! {ex.Message}";
            }
        }


        public string GenerateToken(string email, int userId)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",email),
                new Claim("UserId",userId.ToString())
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string deleteUser(int userId)
        {
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                connection.Open();
                int rows=cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    return "user Deleted SucessFully";
                }
            }
            return "user not deleted";
        }


        public List<userEntity> getAllUsers()
        {
            List<userEntity> users = new List<userEntity>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllUsers", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userEntity user = new userEntity()
                            {
                                userId = Convert.ToInt32(reader["userId"]),
                                userName = reader["userName"].ToString(),
                                email = reader["email"].ToString(),
                                password = reader["password"].ToString(),
                                mobnum = reader["mobnum"].ToString()
                            };
                            users.Add(user);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions here, such as logging the error or throwing it further
                    Console.WriteLine("Error retrieving users: " + ex.Message);
                }
            }
            return users;
        }


        public userEntity getUserDetails(int userId)
        {
            userEntity user = new userEntity();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId",userId);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userEntity userData = new userEntity()
                        {
                            userId = Convert.ToInt32(reader["userID"]),
                            userName = reader["userName"].ToString(),
                            email = reader["email"].ToString(),
                            password = reader["password"].ToString(),
                            mobnum = reader["mobnum"].ToString()

                        };
                        user = userData;
                    }
                }
            }
            if(user!=null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

    }
}
