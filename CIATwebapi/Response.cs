using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CIATwebapi
{
    public class Response
    {
        public string? Result { get; set; }
        public string? Message { get; set; }
        public List<Ticket>? Tickets { get; set; }

        public List<Customer>? Customers { get; set; }
        public List<User>? Users { get; set; }
    }
}