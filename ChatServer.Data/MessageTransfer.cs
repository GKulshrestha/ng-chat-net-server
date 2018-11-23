using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data
{
    class MessageTransfer
    {
        public int MessageTransferId { get; set; }
        public DateTime Sent { get; set; }
        public DateTime Received { get; set; }



    }
}
