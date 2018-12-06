using ChatServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatServer.Controllers
{
    public class UserController : ApiController
    {
        public UserController()
        {

        }
        public void Post(User user)
        {
            var contxt = new ChatDbContext();
            contxt.Users.Add(user);
        }

        public IEnumerable<User> Get()
        {
            var contxt = new ChatDbContext();
            return contxt.Users;
        }
    }

}
