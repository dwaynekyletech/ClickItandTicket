using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CIATwebapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public CustomerController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    static string GetConnectionString()
    {
        string serverName = @"DK-LAPTOP\SQLEXPRESS"; //Change to the "Server Name" you see when you launch SQL Server Management Studio.
        string databaseName = "ClickItandTicket"; //Change to the database where you created your Employee table.
        string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";
        return connectionString;
    }

    [HttpGet]
    [Route("/GetCustomer")]

    public IEnumerable<Customer> Get()
    {
        List<Customer> customers = new List<Customer>();
        string connectionString = GetConnectionString();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            customers = Customer.GetCustomer(sqlConnection);
        }

        return customers;
    }

    [HttpGet]
    [Route("/InsertCustomer")]
    public Response InsertCustomer(string userName, string password)
    {
        Response response = new Response();
        try
        {
            List<Customer> customers = new List<Customer>();

            Customer customer = new Customer(userName, password);

            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = Customer.InsertCustomer(customer, sqlConnection);
                // ticket = Ticket.SearchTicket(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.Customers = customers;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }

        return response;
    }

    [HttpPost]
    [Route("/SearchCustomers")]
    public Response SearchCustomers([FromBody] Customer customer)
    {
        Response response = new Response();
        try
        {
            List<Customer> customers = new List<Customer>();

            string? searchCustomerUsername = customer.userName;
            string? searchCustomerPassword = customer.password;
            int customerid = customer.customer_id;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                customers = CIATwebapi.Customer.SearchCustomers(sqlConnection, searchCustomerUsername, searchCustomerPassword, customerid);
            }

            string message = "";

            if (customers.Count() == 1)
            {
                int customercount = customers[0].CustomerCount;
                message = $"Found {customercount} Users!";
                response.Result = "success";

                customerid = customers[0].customer_id;
                return new Response { Customer_id = customerid };

            }
            else
            {
                response.Result = "success...but";
                message = "Incorrect Username or Password.";
            }


            response.Message = message;
            response.Customers = customers;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }
        return response;
    }
}