using Microsoft.EntityFrameworkCore;
using ShopApp.EmailsAPI.EmailsAPIData;
using ShopApp.EmailsAPI.Models;
using ShopApp.EmailsAPI.Models.Dtos;

namespace ShopApp.EmailsAPI.Repository
{
    public class EmailRepository : IEmailRepository
                 
    {
        private readonly DbContextOptions<EmailsDbContext> _dbContext;

        public EmailRepository(DbContextOptions<EmailsDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SendAndLogEmail(PaymentUpdateMessageDto messageDto)
        {
            //Implement an Email sender o call other class library
            var emailLog = new EmailLog()
            {
                Email = messageDto.Email,
                EmailDate = DateTime.Now,
                Log = $"Order - {messageDto.OrderId} has been created successfully."
            };

            await using var _db = new EmailsDbContext(_dbContext);
            _db.EmailLogs.Add(emailLog);
            await _db.SaveChangesAsync();
        }
    }
}
