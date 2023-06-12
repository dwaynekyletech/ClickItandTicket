using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CIATwebapi.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketUpdateController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public TicketUpdateController(ILogger<WeatherForecastController> logger)
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
    [Route("/GetTicketUpdates")]

    public IEnumerable<TicketUpdate> Get()
    {
        List<TicketUpdate> ticketUpdates = new List<TicketUpdate>();
        string connectionString = GetConnectionString();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            ticketUpdates = TicketUpdate.GetTicketUpdates(sqlConnection);
        }

        return ticketUpdates;
    }
}