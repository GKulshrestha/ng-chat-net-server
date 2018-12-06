using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServer.Models
{
    public class MessageData
    {
        public string Text { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime SentDateTime { get; set; }
    }
}