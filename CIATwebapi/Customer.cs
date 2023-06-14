using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace CIATwebapi
{
    public class Customer
    {
        public int customer_id { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }

        public Customer()
        {

        }
        public Customer(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public Customer(int customer_id, string userName, string password)
        {
            this.customer_id = customer_id;
            this.userName = userName;
            this.password = password;
        }

        public static List<Customer> GetCustomer(SqlConnection sqlConnection)
        {
            List<Customer> customers = new List<Customer>();

            string sql = "select CustomerId, Username, Password from Customer;";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Customer customer = new Customer();
                customer.customer_id = Convert.ToInt32(sqlDataReader["CustomerId"].ToString());
                customer.userName = (sqlDataReader["Username"].ToString());
                customer.password = (sqlDataReader["Password"].ToString());

                customers.Add(customer);
            }

            return customers;
        }

        public static int InsertCustomer(Customer customer, SqlConnection sqlConnection)
        {
            string sql = "insert into Customer (Username, Password) values (@Username, @Password);";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramUsername = new SqlParameter("@Username", customer.userName);
            SqlParameter paramPassword = new SqlParameter("@Password", customer.password);

            paramUsername.DbType = System.Data.DbType.String;
            paramPassword.DbType = System.Data.DbType.String;

            sqlCommand.Parameters.Add(paramUsername);
            sqlCommand.Parameters.Add(paramPassword);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

    }
}