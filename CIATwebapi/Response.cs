using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CIATwebapi
{
    public class Response
    {
        public string? Result { get; set; }
        public string? Message { get; set; }
        public int User_id { get; set; }
        public int Customer_id { get; set; }
        public string? UpdateContent { get; set; }
        public List<string?>? Updates { get; set; }
        public List<Ticket>? Tickets { get; set; }

        public List<Customer>? Customers { get; set; }
        public List<User>? Users { get; set; }
        public List<TicketUpdate>? ticketUpdates { get; set; }
    }
}