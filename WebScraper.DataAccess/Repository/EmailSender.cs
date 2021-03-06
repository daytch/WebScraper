using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace WebScraper.DataAccess.Repository
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message) => Task.CompletedTask;
    }
}
