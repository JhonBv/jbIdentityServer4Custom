using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Services
{
    public class EmailService : IIdentityMessageService
    {
        public IConfiguration Configuration { get; }
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        private async Task configSendGridasync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();

            myMessage.AddTo(message.Destination);
            //JB. Sender's details
            myMessage.From = new System.Net.Mail.MailAddress(
                               "admin@creditgateway.com",
                               "Credit Passport Gateway");//Configuration.GetValue<string>("EmailSender:RegistrationsAdminEmail"),
                                //Configuration.GetValue<string>("EmailSender:RegistrationsAdminFrom"));
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;

            //string[] dbcons = System.IO.File.ReadAllLines(@"EmailTextPss.txt");

            var credentials = new System.Net.NetworkCredential(
                       //JB. Password is taken from the Azure portal.
                       "azure_766fe05f66a5a5b372dd90eae4dbfef5@azure.com",
                       "!@mD@us3r"//Configuration.GetValue<string>("EmailSender:MailPassword")
            //Set at the IIS root instead!
            //dbcons[0].ToString()

                       );

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            if (transportWeb != null)
            {
                await transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }
        }
    }
}
