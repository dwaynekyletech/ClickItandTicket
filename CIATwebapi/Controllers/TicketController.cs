
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

    public Response GetTickets(string customer_id)
    {
        Response response = new Response();
        try
        {
            List<Ticket> tickets = new List<Ticket>();

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                tickets = Ticket.GetTickets(Convert.ToInt32(customer_id), sqlConnection);
            }

            string message = "";

            if (tickets.Count() > 0)
            {
                int ticketCount = tickets[0].ticketCount;
                message = $"Found {ticketCount} Tickets!";
            }
            else
            {
                message = "No tickets met your search criteria.";
            }

            response.Result = "success";
            response.Message = message;
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
    [Route("/GetAllTickets")]

    public Response GetAllTickets(int user_id)
    {
        Response response = new Response();
        try
        {
            List<Ticket> tickets = new List<Ticket>();

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                tickets = Ticket.GetAllTickets(user_id, sqlConnection);
            }

            string message = "";

            if (tickets.Count() > 0)
            {
                int ticketCount = tickets[0].ticketCount;
                message = $"Found {ticketCount} Tickets!";
            }
            else
            {
                message = "No tickets met your search criteria.";
            }

            response.Result = "success";
            response.Message = message;
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
    [Route("/InsertTicket")]
    public Response InsertTicket(string ticketSubject, string ticketDescription, TicketStatus ticketStatus, TicketPriority ticketPriority, int customerid)
    {
        Response response = new Response();
        try
        {
            List<Ticket> tickets = new List<Ticket>();

            Ticket ticket = new Ticket(ticketSubject, ticketDescription, ticketStatus, ticketPriority, customerid);

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

    [HttpGet]
    [Route("/UpdateTicket")]
    public Response UpdateTicket(string ticket_id, string? ticketSubject, string? ticketDescription)
    {
        Response response = new Response();

        try
        {
            List<Ticket> tickets = new List<Ticket>();


            Ticket ticket = new Ticket(Convert.ToInt32(ticket_id), ticketSubject, ticketDescription);

            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = Ticket.UpdateTicket(ticket, sqlConnection);
                // tickets = Ticket.SearchTickets(sqlConnection);
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

    [HttpGet]
    [Route("/DeleteTicket")]
    public Response DeleteTicket(string ticket_id)
    {
        Response response = new Response();

        try
        {
            List<Ticket> tickets = new List<Ticket>();
            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = Ticket.DeleteTicket(Convert.ToInt32(ticket_id), sqlConnection);
                // tickets = Ticket.SearchTickets(sqlConnection);
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


