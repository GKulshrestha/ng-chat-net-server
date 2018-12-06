using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChatServer.Controllers;
using ChatServer.Models;
using Moq;
using ChatServer.Data;
using System.Data.Entity;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ChatServer.Tests.Controllers
{
    [TestClass]
    public class MessagesController_UnitTests
    {
        #region Fields
        MessagesController _controller;
        Mock<IChatDbContext> _mockContext;
        Mock<DbSet<Message>> _mockDbSetMessage;
        Message[] _messages = new Message[] {
            new Message { MessageId=101,MessageText="Hi",SenderId=1, ReceiverId=2,Sent=DateTime.Now.AddSeconds(-10)},
            new Message { MessageId=103,MessageText="How are you?",SenderId=1, ReceiverId=2,Sent=DateTime.Now.AddSeconds(-9)},
            new Message { MessageId=102,MessageText="I am fine",SenderId=2, ReceiverId=1,Sent=DateTime.Now.AddSeconds(-7)},
            new Message { MessageId=104,MessageText="How about you?",SenderId=2, ReceiverId=1,Sent=DateTime.Now.AddSeconds(-6)},
            new Message { MessageId=106,MessageText="Doing good",SenderId=1, ReceiverId=2,Sent=DateTime.Now.AddSeconds(-4)},
            new Message { MessageId=109,MessageText="Let's catch up some time",SenderId=2, ReceiverId=1,Sent=DateTime.Now.AddSeconds(-1)},
            new Message { MessageId=107,MessageText="Hi",SenderId=2, ReceiverId=3,Sent=DateTime.Now.AddSeconds(-3)},
            new Message { MessageId=108,MessageText="How are you?",SenderId=3, ReceiverId=1,Sent=DateTime.Now.AddSeconds(-3)},
        };

        #endregion
        [TestInitialize]
        public void Setup()
        {
            _mockContext = new Mock<IChatDbContext>(MockBehavior.Strict);
            _mockDbSetMessage = GetQueryableMockDbSet<Message>(_messages.ToList());
            _mockContext.Setup(mc => mc.Messages).Returns(_mockDbSetMessage.Object);
            _mockContext.Setup(lm => lm.SaveChanges()).Returns(1);
            _controller = new MessagesController(_mockContext.Object);
        }
        #region Tests
        [TestMethod]
        public void Post_SavesMessageToDB()
        {
            DateTime sendDateTime = DateTime.Now;
            MessageData msgData = new MessageData() { SenderId = 1, ReceiverId = 2, SentDateTime = sendDateTime };
            _mockContext.Setup(lm => lm.Messages.Add(It.Is<Message>(k => k.SenderId == msgData.SenderId && k.ReceiverId == msgData.ReceiverId && k.Sent == sendDateTime))).Returns(new Message()
                ).Verifiable();
            _controller.Post(msgData);
            _mockDbSetMessage.Verify();
            _mockContext.Verify(lm => lm.SaveChanges());

        }
        [TestMethod]
        public void Get_ReturnsAllCoversationsOfGivenUsers()
        {
            int senderId = 1, receiverId = 2;
            var messages = _controller.Get(senderId, receiverId);
           CollectionAssert.AreEqual(messages.ToArray(), _messages.Where(msg => (msg.SenderId == senderId && msg.ReceiverId == receiverId)
              || (msg.SenderId == receiverId && msg.ReceiverId == senderId)).OrderBy(msg => msg.Sent).ToArray());
        }
        #endregion

        #region Helper Methods
        private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return dbSet;
        }
        #endregion
    }
}
