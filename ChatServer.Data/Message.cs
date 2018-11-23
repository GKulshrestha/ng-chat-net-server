using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data
{
    class Message
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public bool IsDeleted { get; set; }

    }
}
