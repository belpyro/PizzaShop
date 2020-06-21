using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RestSample.Logic
{
    internal class PizzaEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // concrete implementation for smtp client
            // Mailgun
            return Task.CompletedTask;
        }
    }
}