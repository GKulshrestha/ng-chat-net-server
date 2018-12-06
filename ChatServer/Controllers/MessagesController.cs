using ChatServer.Data;
using ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ChatServer.Controllers
{
    public class MessagesController : ApiController
    {
        #region Fields
        IChatDbContext _context;
        #endregion

        public MessagesController(IChatDbContext context)
        {
            _context = context;
        }
        [Route("api/Messages/{senderId}/{receiverId}")]
        public IEnumerable<Message> Get(int senderId, int receiverId)
        {
              var k=_context.Messages.Where(msg => (msg.SenderId == senderId && msg.ReceiverId == receiverId)
              || (msg.SenderId == receiverId && msg.ReceiverId == senderId)).OrderBy(msg => msg.Sent);
            return k;
         
        }
        //todo: handle multiple simultaneous posts
        public void Post(MessageData data)
        {
            Message msgToAdd = new Message { MessageText = data.Text, SenderId = data.SenderId, ReceiverId = data.ReceiverId, Sent = data.SentDateTime };
            _context.Messages.Add(msgToAdd);
            _context.SaveChanges();
        }

    }
}