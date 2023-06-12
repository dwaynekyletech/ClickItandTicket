using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace CIATwebapi
{
    public class TicketUpdate : Ticket
    {
        public int update_id { get; set; }
        public DateTime updateTicketTimestamp { get; set; }
        public string? updateContent { get; set; }

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
    }


}