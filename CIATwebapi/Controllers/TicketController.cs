
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CIATwebapi.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public TicketController(ILogger<WeatherForecastController> logger)
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
    [Route("/GetTickets")]

    public IEnumerable<Ticket> Get()
    {
        List<Ticket> tickets = new List<Ticket>();
        string connectionString = GetConnectionString();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            tickets = Ticket.GetTickets(sqlConnection);
        }

        return tickets;
    }


    [HttpGet]
    [Route("/InsertTicket")]
    public Response InsertTicket(string ticketDescription, TicketStatus ticketStatus, TicketPriority ticketPriority)
    {
        Response response = new Response();
        try
        {
            List<Ticket> tickets = new List<Ticket>();

            Ticket ticket = new Ticket(ticketDescription, ticketStatus, ticketPriority);

            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = Ticket.InsertTicket(ticket, sqlConnection);
                // ticket = Ticket.SearchTicket(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.Tickets = tickets;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }

        return response;
    }

}


