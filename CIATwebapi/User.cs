using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace CIATwebapi
{
    public class User
    {
        public int user_id { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }

        public User()
        {

        }
        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public User(int user_id, string userName, string password)
        {
            this.user_id = user_id;
            this.userName = userName;
            this.password = password;
        }

        public static List<User> GetUsers(SqlConnection sqlConnection)
        {
            List<User> users = new List<User>();

            string sql = "select UserId, Username, Password from [User];";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                User user = new User();
                user.user_id = Convert.ToInt32(sqlDataReader["UserId"].ToString());
                user.userName = (sqlDataReader["Username"].ToString());
                user.password = (sqlDataReader["Password"].ToString());

                users.Add(user);
            }

            return users;
        }

        public static int InsertUser(User user, SqlConnection sqlConnection)
        {
            string sql = "insert into [User] (Username, Password) values (@Username, @Password);";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramUserName = new SqlParameter("@Username", user.userName);
            SqlParameter paramPassword = new SqlParameter("@Password", user.password);

            paramUserName.DbType = System.Data.DbType.String;
            paramPassword.DbType = System.Data.DbType.String;

            sqlCommand.Parameters.Add(paramUserName);
            sqlCommand.Parameters.Add(paramPassword);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

    }
}