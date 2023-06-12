using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace CIATwebapi
{
    public class Customer
    {
        public int customer_id { get; set; }
        public string? customerName { get; set; }
        public string? customerEmail { get; set; }

        public Customer()
        {

        }
        public Customer(string customerName, string customerEmail)
        {
            this.customerName = customerName;
            this.customerEmail = customerEmail;
        }

        public Customer(int customer_id, string customerName, string customerEmail)
        {
            this.customer_id = customer_id;
            this.customerName = customerName;
            this.customerEmail = customerEmail;
        }

        public static List<Customer> GetCustomer(SqlConnection sqlConnection)
        {
            List<Customer> customers = new List<Customer>();

            string sql = "select CustomerId, CustomerName, CustomerEmail from Customer;";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Customer customer = new Customer();
                customer.customer_id = Convert.ToInt32(sqlDataReader["CustomerId"].ToString());
                customer.customerName = (sqlDataReader["CustomerName"].ToString());
                customer.customerEmail = (sqlDataReader["CustomerEmail"].ToString());

                customers.Add(customer);
            }

            return customers;
        }

        public static int InsertCustomer(Customer customer, SqlConnection sqlConnection)
        {
            string sql = "insert into Customer (CustomerName, CustomerEmail) values (@CustomerName, @CustomerEmail);";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramCustomerName = new SqlParameter("@CustomerName", customer.customerName);
            SqlParameter paramCustomerEmail = new SqlParameter("@CustomerEmail", customer.customerEmail);

            paramCustomerName.DbType = System.Data.DbType.String;
            paramCustomerEmail.DbType = System.Data.DbType.String;

            sqlCommand.Parameters.Add(paramCustomerName);
            sqlCommand.Parameters.Add(paramCustomerEmail);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

    }
}