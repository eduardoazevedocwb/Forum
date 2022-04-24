using Microsoft.EntityFrameworkCore;

namespace Forum.Models
{
    public class ForumContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TopicLog> TopicLogs { get; set; }
        public DbSet<LogonLog> LogonLogs { get; set; }
        public DbSet<Logon> Logon { get; set; }

        public ForumContext (DbContextOptions <ForumContext> options) : base(options) { }

    }
}
