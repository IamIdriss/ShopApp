using Microsoft.EntityFrameworkCore;
using ShopApp.EmailsAPI.Models;

namespace ShopApp.EmailsAPI.EmailsAPIData
{
    public class EmailsDbContext : DbContext
    {
        public EmailsDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<EmailLog> EmailLogs { get; set; }
    }
}
