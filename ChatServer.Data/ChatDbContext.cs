using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data
{
    public class ChatDbContext : DbContext, IChatDbContext
    {
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public ChatDbContext() : base(ConfigurationManager.ConnectionStrings["ChatDbContext"].ConnectionString)
        {
            this.Database.Log = printer;
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        private void printer(string txt)
        {
            System.Diagnostics.Debug.WriteLine(txt);
        }
    }
}
