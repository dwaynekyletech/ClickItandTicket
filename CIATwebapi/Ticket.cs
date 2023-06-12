using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json.Serialization;


namespace CIATwebapi
{
    public class Ticket
    {
        public int ticket_id { get; private set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketPriority ticketPriority { get; private set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketStatus ticketStatus { get; private set; }
        public string? ticketDescription { get; private set; }

        public Ticket()
        {

        }

        public Ticket(string ticketDescription, TicketStatus ticketStatus, TicketPriority ticketPriority)
        {
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

        public static List<Ticket> GetTickets(SqlConnection sqlConnection)
        {
            List<Ticket> tickets = new List<Ticket>();

            string sql = "select TicketId, PriorityId, StatusId, Description from Ticket;";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Ticket ticket = new Ticket();
                ticket.ticket_id = Convert.ToInt32(sqlDataReader["TicketId"].ToString());
                ticket.ticketPriority = (TicketPriority)sqlDataReader["PriorityId"];
                ticket.ticketStatus = (TicketStatus)(sqlDataReader["StatusId"]);
                ticket.ticketDescription = (sqlDataReader["Description"].ToString());

                tickets.Add(ticket);
            }

            return tickets;
        }

        public static int InsertTicket(Ticket ticket, SqlConnection sqlConnection)
        {
            string sql = "insert into Ticket (Description, StatusId, PriorityId) values (@Description, @StatusId, @PriorityId);";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramDescription = new SqlParameter("@Description", ticket.ticketDescription);
            SqlParameter paramStatus = new SqlParameter("@StatusId", ticket.ticketStatus);
            SqlParameter paramPriority = new SqlParameter("@PriorityId", ticket.ticketPriority);

            paramDescription.DbType = System.Data.DbType.String;
            paramStatus.DbType = System.Data.DbType.Int32;
            paramPriority.DbType = System.Data.DbType.Int32;

            sqlCommand.Parameters.Add(paramDescription);
            sqlCommand.Parameters.Add(paramStatus);
            sqlCommand.Parameters.Add(paramPriority);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }





    }
}