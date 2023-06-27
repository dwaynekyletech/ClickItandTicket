using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace CIATwebapi
{
    public class TicketUpdate
    {
        public int update_id { get; set; }
        public DateTime updateTicketTimestamp { get; set; }
        public string? updateContent { get; set; }
        public int user_id { get; set; }
        public int ticket_id { get; set; }
        public int ticketCount { get; set; }

        public TicketUpdate()
        {
        }
        public TicketUpdate(int user_id, string? updateContent, DateTime updateTicketTimestamp)
        {
            this.user_id = user_id;
            this.updateContent = updateContent;
            this.updateTicketTimestamp = updateTicketTimestamp;
        }
        public static List<TicketUpdate> GetTicketUpdates(SqlConnection sqlConnection)
        {
            List<TicketUpdate> ticketUpdates = new List<TicketUpdate>();

            string sql = "select UpdateId, [TimeStamp], UpdateContent from TicketUpdate;";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                TicketUpdate ticketUpdate = new TicketUpdate();
                ticketUpdate.update_id = Convert.ToInt32(sqlDataReader["UpdateId"].ToString());
                ticketUpdate.updateTicketTimestamp = Convert.ToDateTime(sqlDataReader["TimeStamp"]);
                ticketUpdate.updateContent = (sqlDataReader["UpdateContent"].ToString());

                ticketUpdates.Add(ticketUpdate);
            }

            return ticketUpdates;
        }

        public static int InsertUpdateTicket(TicketUpdate ticketUpdate, Ticket ticket, SqlConnection sqlConnection)
        {
            string sql = "Begin Transaction; insert into TicketUpdate (UserId, UpdateContent, TicketId, [TimeStamp]) values (@UserId, @UpdateContent, @TicketId, CURRENT_TIMESTAMP); Update Ticket Set StatusId = @StatusId, UserId = @UserId Where TicketId = @TicketId; COMMIT;";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramUserId = new SqlParameter("@UserId", ticketUpdate.user_id);
            SqlParameter paramUpdateContent = new SqlParameter("@UpdateContent", ticketUpdate.updateContent);
            SqlParameter paramStatus = new SqlParameter("@StatusId", ticket.ticketStatus);
            SqlParameter paramTicketId = new SqlParameter("@TicketId", ticket.ticket_id);

            paramUserId.DbType = System.Data.DbType.Int32;
            paramUpdateContent.DbType = System.Data.DbType.String;
            paramStatus.DbType = System.Data.DbType.Int32;
            paramTicketId.DbType = System.Data.DbType.Int32;

            sqlCommand.Parameters.Add(paramUserId);
            sqlCommand.Parameters.Add(paramUpdateContent);
            sqlCommand.Parameters.Add(paramStatus);
            sqlCommand.Parameters.Add(paramTicketId);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        public static List<TicketUpdate> GetTicketUpdate(int ticket_id, SqlConnection sqlConnection)
        {
            List<TicketUpdate> ticketUpdates = new List<TicketUpdate>();
            string sql = "select TicketId, UpdateContent, count(*) over () AS [Count] from TicketUpdate where TicketId = @ticket_id";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlParameter paramCustomerId = new SqlParameter("@ticket_id", ticket_id);
            paramCustomerId.DbType = System.Data.DbType.Int32;
            sqlCommand.Parameters.Add(paramCustomerId);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                TicketUpdate ticketUpdate = new TicketUpdate();
                ticketUpdate.ticket_id = Convert.ToInt32(sqlDataReader["TicketId"].ToString());
                ticketUpdate.updateContent = (sqlDataReader["UpdateContent"].ToString());
                ticketUpdate.ticketCount = Convert.ToInt32(sqlDataReader["Count"].ToString());

                ticketUpdates.Add(ticketUpdate);
            }

            return ticketUpdates;
        }
    }





}