using System;
using System.Collections.Generic;

namespace FreelanceExchange_desktop.Data
{
    public class Response
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int FreelancerId { get; set; }
        public string Message { get; set; }
        public decimal ProposedPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsSelected { get; set; }
        public List<Task> Tasks { get; set; }
    }

}