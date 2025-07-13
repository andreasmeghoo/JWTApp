using Microsoft.EntityFrameworkCore;
using JWTApp.Models;

namespace JWTApp.Data
{
    public class JWTAppDBContext : DbContext
    {
        public JWTAppDBContext(DbContextOptions<JWTAppDBContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
    }
}
