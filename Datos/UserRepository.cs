using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Datos
{
    public class UserRepository
    {
        private readonly SqlConnection _connection;
        public UserRepository(ConnectionManager connection)
        {
            _connection = connection._conexion;
        }
        public void Guardar(User user)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"Insert Into [dbo].[User] (UserName,FirstName,LastName,Password,MobilePhone,Email,Estado) 
                                        values (@UserName,@FirstName,@LastName,@Password,@MobilePhone,@Email,@Estado)";
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@MobilePhone", user.MobilePhone);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Estado", user.Estado);
                var filas = command.ExecuteNonQuery();
            }
        }
        public User Validate(string userName, string password)
        {
            SqlDataReader dataReader;
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select * from [dbo].[User] where userName=@userName and password=@password";
                command.Parameters.AddWithValue("@userName", userName);
                command.Parameters.AddWithValue("@password", password);
                dataReader = command.ExecuteReader();
                dataReader.Read();
                return DataReaderMapToUser(dataReader);
            }
        }
        private User DataReaderMapToUser(SqlDataReader dataReader)
        {
            if(!dataReader.HasRows) return null;
            User user = new User();
            user.UserName = (string)dataReader["UserName"];
            user.FirstName = (string)dataReader["FirstName"];
            user.LastName = (string)dataReader["LastName"];
            user.Password = (string)dataReader["Password"];
            user.MobilePhone = (string)dataReader["MobilePhone"];
            user.Email = (string)dataReader["Email"];
            user.Estado = (string)dataReader["Estado"];
            return user;
        }
    }
}