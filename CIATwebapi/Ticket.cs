using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json.Serialization;


namespace CIATwebapi
{
    public class Ticket
    {
        public int ticket_id { get; private set; }
        public string? ticketSubject { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketPriority ticketPriority { get; private set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketStatus ticketStatus { get; private set; }
        public string? ticketDescription { get; private set; }
        public int ticketCount { get; set; }
        public string? customer_id { get; set; }

        public Ticket()
        {

        }
        public Ticket(string customer_id)
        {
            this.customer_id = customer_id;
        }

        public Ticket(string ticketSubject, string ticketDescription, TicketStatus ticketStatus, TicketPriority ticketPriority)
        {
            this.ticketSubject = ticketSubject;
            this.ticketDescription = ticketDescription;
            this.ticketStatus = TicketStatus.Open;
            this.ticketPriority = ticketPriority;
        }

        public Ticket(int ticket_id, string ticketDescription, TicketStatus ticketStatus, TicketPriority ticketPriority)
        {
            this.ticket_id = ticket_id;
            this.ticketDescription = ticketDescription;
            this.ticketStatus = ticketStatus;
            this.ticketPriority = ticketPriority;
        }

        public static List<Ticket> GetTickets(int customer_id, SqlConnection sqlConnection)
        {
            List<Ticket> tickets = new List<Ticket>();
            string sql = "select TicketId, TicketSubject, PriorityId, StatusId, Description, count(*) over () AS [Count] from Ticket where CustomerId = @customer_id";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlParameter paramCustomerId = new SqlParameter("@customer_id", customer_id);
            paramCustomerId.DbType = System.Data.DbType.Int32;
            sqlCommand.Parameters.Add(paramCustomerId);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Ticket ticket = new Ticket();
                ticket.ticket_id = Convert.ToInt32(sqlDataReader["TicketId"].ToString());
                ticket.ticketSubject = (sqlDataReader["TicketSubject"].ToString());
                ticket.ticketPriority = (TicketPriority)sqlDataReader["PriorityId"];
                ticket.ticketStatus = (TicketStatus)(sqlDataReader["StatusId"]);
                ticket.ticketDescription = (sqlDataReader["Description"].ToString());
                ticket.ticketCount = Convert.ToInt32(sqlDataReader["Count"].ToString());

                tickets.Add(ticket);
            }

            return tickets;
        }

        public static int InsertTicket(Ticket ticket, SqlConnection sqlConnection)
        {
            string sql = "insert into Ticket (TicketSubject, Description, StatusId, PriorityId) values (@Subject, @Description, @StatusId, @PriorityId);";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramSubject = new SqlParameter("@Subject", ticket.ticketSubject);
            SqlParameter paramDescription = new SqlParameter("@Description", ticket.ticketDescription);
            SqlParameter paramStatus = new SqlParameter("@StatusId", ticket.ticketStatus);
            SqlParameter paramPriority = new SqlParameter("@PriorityId", ticket.ticketPriority);

            paramSubject.DbType = System.Data.DbType.String;
            paramDescription.DbType = System.Data.DbType.String;
            paramStatus.DbType = System.Data.DbType.Int32;
            paramPriority.DbType = System.Data.DbType.Int32;

            sqlCommand.Parameters.Add(paramSubject);
            sqlCommand.Parameters.Add(paramDescription);
            sqlCommand.Parameters.Add(paramStatus);
            sqlCommand.Parameters.Add(paramPriority);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        public static int DeleteTicket(int ticket_id, SqlConnection sqlConnection)
        {
            string sql = "delete from Ticket where TicketId = @TicketId;";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramTicketId = new SqlParameter("@TicketId", ticket_id);
            paramTicketId.DbType = System.Data.DbType.Int32;
            sqlCommand.Parameters.Add(paramTicketId);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }




    }
}