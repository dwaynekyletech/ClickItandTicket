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
        public int CustomerCount { get; set; }

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

        public static List<Customer> SearchCustomers(SqlConnection sqlConnection, string? searchCustomerUsername, string? searchCustomerPassword, int customerid)
        {
            List<Customer> customers = new List<Customer>();

            string sql = "select x.CustomerId, u.Username, u.Password, x.[Count] from (select CustomerId, COUNT (*) over() as [Count] from [Customer] where Username = @Search COLLATE Latin1_General_CS_AS and Password = @Search2 COLLATE Latin1_General_CS_AS)x join [Customer] u on x.CustomerId = u.CustomerId order by 1;";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlParameter paramSearch = new SqlParameter("@Search", searchCustomerUsername);
            SqlParameter paramSearch2 = new SqlParameter("@Search2", searchCustomerPassword);
            paramSearch.DbType = System.Data.DbType.String;
            paramSearch2.DbType = System.Data.DbType.String;
            sqlCommand.Parameters.Add(paramSearch);
            sqlCommand.Parameters.Add(paramSearch2);


            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Customer customer = new Customer();
                customer.customer_id = Convert.ToInt32(sqlDataReader["CustomerId"].ToString());
                customer.userName = (sqlDataReader["Username"].ToString());
                customer.password = (sqlDataReader["Password"].ToString());
                customer.CustomerCount = Convert.ToInt32(sqlDataReader["Count"].ToString());

                customers.Add(customer);
            }

            return customers;
        }

        public static int InsertCustomer(SqlConnection sqlConnection, string? insertCustomerUsername, string? insertCustomerPassword)
        {
            List<Customer> customers = new List<Customer>();

            string sql = "insert into Customer (Username, Password) values (@Username, @Password);";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlParameter paramSearch = new SqlParameter("@Username", insertCustomerUsername);
            SqlParameter paramSearch2 = new SqlParameter("@Password", insertCustomerPassword);
            paramSearch.DbType = System.Data.DbType.String;
            paramSearch2.DbType = System.Data.DbType.String;
            sqlCommand.Parameters.Add(paramSearch);
            sqlCommand.Parameters.Add(paramSearch2);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }
    }
}