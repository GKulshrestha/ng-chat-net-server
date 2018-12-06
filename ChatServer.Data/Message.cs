using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Sent { get; set; }
        public DateTime? Received { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        [ForeignKey("SenderId")]
        public User SenderUser { get; set; }
        [ForeignKey("ReceiverId")]
        public User RecieverUser { get; set; }


    }
}
