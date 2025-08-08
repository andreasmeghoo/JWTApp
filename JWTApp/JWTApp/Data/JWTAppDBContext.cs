using Microsoft.EntityFrameworkCore;
using JWTApp.Models;
using JWTApp.Models.Logging;

namespace JWTApp.Data
{
    public class JWTAppDBContext : DbContext
    {
        public JWTAppDBContext(DbContextOptions<JWTAppDBContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        public DbSet<LogEntry> ApiLogs =>Set<LogEntry>();
    }
}
