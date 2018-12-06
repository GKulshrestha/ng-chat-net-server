using System.Data.Entity;

namespace ChatServer.Data
{
    public interface IChatDbContext
    {
        DbSet<Message> Messages { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}