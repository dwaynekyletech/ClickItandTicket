using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CIATwebapi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public UserController(ILogger<WeatherForecastController> logger)
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
    [Route("/GetUser")]

    public IEnumerable<User> Get()
    {
        List<User> users = new List<User>();
        string connectionString = GetConnectionString();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            users = CIATwebapi.User.GetUsers(sqlConnection);
        }

        return users;
    }

    [HttpGet]
    [Route("/InsertUser")]
    public Response InsertUser(string userName, string password)
    {
        Response response = new Response();
        try
        {
            List<User> users = new List<User>();

            User user = new User(userName, password);



            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = CIATwebapi.User.InsertUser(user, sqlConnection);
                // ticket = Ticket.SearchTicket(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.Users = users;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }

        return response;
    }

    [HttpGet]
    [Route("/SearchUsers")]
    public Response SearchUsers(string search = "", string search2 = "")
    {
        Response response = new Response();
        try
        {
            List<User> users = new List<User>();

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                users = CIATwebapi.User.SearchUsers(sqlConnection, search, search2);
            }

            string message = "";

            if (users.Count() == 1)
            {
                int userCount = users[0].UserCount;
                message = $"Found {userCount} Users!";
                response.Result = "success";

            }
            else
            {
                response.Result = "success...but";
                message = "Incorrect Username or Password.";
            }


            response.Message = message;
            response.Users = users;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }
        return response;
    }
}