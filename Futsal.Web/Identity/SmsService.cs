﻿using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Futsal.Web.Identity
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message) =>
            // Plug in your SMS service here to send a text message.
            Task.FromResult(0);
    }
}