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

    [HttpGet]
    [Route("/InsertUpdateTicket")]
    public Response InsertUpdateTicket(int user_id, string updateContent, DateTime updateTicketTimestamp, TicketStatus ticketStatus, int ticket_id)
    {
        Response response = new Response();
        try
        {
            List<TicketUpdate> ticketUpdates = new List<TicketUpdate>();
            List<Ticket> tickets = new List<Ticket>();
            TicketUpdate ticketUpdate = new TicketUpdate(user_id, updateContent, updateTicketTimestamp);
            Ticket ticket = new Ticket(ticket_id, ticketStatus);

            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = TicketUpdate.InsertUpdateTicket(ticketUpdate, ticket, sqlConnection);
                // ticket = Ticket.SearchTicket(sqlConnection);
            }

            response.Result = (rowsAffected == 2) ? "success" : "failure";
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

    [HttpGet]
    [Route("/getTicketUpdate")]

    public Response GetTicketUpdate(int ticket_id)
    {
        Response response = new Response();
        try
        {
            List<TicketUpdate> ticketUpdates;
            string connectionString = GetConnectionString();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                ticketUpdates = TicketUpdate.GetTicketUpdate(ticket_id, sqlConnection);
            }

            string message = "";

            if (ticketUpdates.Count() > 0)
            {

                List<string?> updateContents = ticketUpdates.Select(tu => tu.updateContent).ToList<string?>();
                int ticketUpdatesCount = updateContents.Count();
                message = $"Found {ticketUpdatesCount} Updates!";

                return new Response { Updates = updateContents ?? new List<string?>() };
            }
            else
            {
                message = "No tickets met your search criteria.";
            }

            response.Result = "success";
            response.Message = message;
            response.ticketUpdates = ticketUpdates;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }
        return response;
    }

}