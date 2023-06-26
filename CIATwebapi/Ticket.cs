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
        public int customerid { get; set; }

        public Ticket()
        {

        }
        public Ticket(string customer_id)
        {
            this.customer_id = customer_id;
        }

        public Ticket(string ticketSubject, string ticketDescription, TicketStatus ticketStatus, TicketPriority ticketPriority, int customerid)
        {
            this.ticketSubject = ticketSubject;
            this.ticketDescription = ticketDescription;
            this.ticketStatus = TicketStatus.Open;
            this.ticketPriority = ticketPriority;
            this.customerid = customerid;
        }
        public Ticket(int ticket_id, string? ticketSubject, string? ticketDescription)
        {
            this.ticket_id = ticket_id;
            this.ticketSubject = ticketSubject;
            this.ticketDescription = ticketDescription;
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

        public static List<Ticket> GetAllTickets(SqlConnection sqlConnection)
        {
            List<Ticket> tickets = new List<Ticket>();
            string sql = "select TicketId, TicketSubject, PriorityId, StatusId, Description, count(*) over () AS [Count] from Ticket";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
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
            string sql = "insert into Ticket (TicketSubject, Description, StatusId, PriorityId, CustomerId) values (@Subject, @Description, @StatusId, @PriorityId, @CustomerId);";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramSubject = new SqlParameter("@Subject", ticket.ticketSubject);
            SqlParameter paramDescription = new SqlParameter("@Description", ticket.ticketDescription);
            SqlParameter paramStatus = new SqlParameter("@StatusId", ticket.ticketStatus);
            SqlParameter paramPriority = new SqlParameter("@PriorityId", ticket.ticketPriority);
            SqlParameter paramCustomerId = new SqlParameter("@CustomerId", ticket.customerid);

            paramSubject.DbType = System.Data.DbType.String;
            paramDescription.DbType = System.Data.DbType.String;
            paramStatus.DbType = System.Data.DbType.Int32;
            paramPriority.DbType = System.Data.DbType.Int32;
            paramCustomerId.DbType = System.Data.DbType.Int32;

            sqlCommand.Parameters.Add(paramSubject);
            sqlCommand.Parameters.Add(paramDescription);
            sqlCommand.Parameters.Add(paramStatus);
            sqlCommand.Parameters.Add(paramPriority);
            sqlCommand.Parameters.Add(paramCustomerId);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        public static int UpdateTicket(Ticket ticket, SqlConnection sqlConnection)
        {
            string sql = "update Ticket set TicketSubject = @TicketSubject, Description = @Description where TicketId = @TicketId;";


            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramTicketSubject = new SqlParameter("@TicketSubject", ticket.ticketSubject == null ? (object)DBNull.Value : ticket.ticketSubject);
            SqlParameter paramDescription = new SqlParameter("@Description", ticket.ticketDescription == null ? (object)DBNull.Value : ticket.ticketDescription);
            SqlParameter paramTicketId = new SqlParameter("@TicketId", ticket.ticket_id);

            paramTicketSubject.DbType = System.Data.DbType.String;
            paramDescription.DbType = System.Data.DbType.String;
            paramTicketId.DbType = System.Data.DbType.Int32;

            sqlCommand.Parameters.Add(paramTicketSubject);
            sqlCommand.Parameters.Add(paramDescription);
            sqlCommand.Parameters.Add(paramTicketId);

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