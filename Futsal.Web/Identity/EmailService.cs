using Microsoft.AspNet.Identity;
using System.Threading.Tasks;


namespace Futsal.Web.Identity
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message) =>
            // Plug in your email service here to send an email.
            Task.FromResult(0);
    }
}