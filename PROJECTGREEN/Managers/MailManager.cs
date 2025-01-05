using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using System;

namespace PROJECTGREEN.Manager
{
    public class MailManager
    {
        private readonly string _mailSender;
        private readonly string _mailAppPassword;

        public MailManager(IConfiguration configuration)
        {
            _mailSender = configuration["EmailSettings:MailSender"];
            _mailAppPassword = configuration["EmailSettings:MailSenderAppPassword"];
        }

        public bool SendEmail(string recipient, string subject, string msgBody, ref string errResponse)
        {
            try
            {
                using (var message = new MailMessage())
                {
                    var smtp = new SmtpClient
                    {
                        Port = 587,
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(_mailSender, _mailAppPassword),
                        DeliveryMethod = SmtpDeliveryMethod.Network
                    };

                    message.From = new MailAddress(_mailSender, "Babag EcoHub");
                    message.To.Add(new MailAddress(recipient));
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    message.Body = msgBody;

                    smtp.Send(message);
                    errResponse = "Message Sent";
                    return true;
                }
            }
            catch (Exception ex)
            {
                errResponse = ex.Message;
                return false;
            }
        }

        public bool SendAlertEmail(string recipientEmail, string subject, string firstName, string schedule, string zone, ref string errResponse)
        {
            string welcomeTemplate = $@"<!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Welcome to Babag EcoHub!</title>
                    </head>
                    <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0;'>
                        <div style='max-width: 600px; margin: 20px auto; background-color: #fff; border-radius: 10px; overflow: hidden;'>
                            <div style='background-color: #70c778; color: #333; text-align: center; padding: 20px 0;'>
                                <h1 style='margin: 0; font-size: 24px;'>Babag EcoHub Waste Collection Reminder!</h1>
                            </div>
                            <div style='padding: 20px;'>
                                <p style='font-size: 16px;'>Hi, {firstName}! This is Babag EcoHub, since you've subscribed to our alert notification, here's a reminder for you to ready up your trash bags!</p>
                                <p style='font-size: 16px;'>Our garbage truck driver is on the way to collect the trashes at {zone}, {schedule}. </p>
                                <p style='font-size: 16px;'>Thank you for helping Brgy. Babag become cleaner!</p>
                            </div>
                        </div>
                    </body>
                    </html>";

            welcomeTemplate = welcomeTemplate.Replace("{firstName}", firstName).Replace("{zone}", zone)
                                     .Replace("{zone}", zone)
                                     .Replace("{schedule}", schedule);

            return SendEmail(recipientEmail, subject, welcomeTemplate, ref errResponse);
        }

        public bool SendAlertAdminEmail(string recipientEmail, string subject, string firstName, string report ,ref string errResponse)
        {
            string welcomeTemplate = $@"<!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Welcome to Babag EcoHub!</title>
                    </head>
                    <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0;'>
                        <div style='max-width: 600px; margin: 20px auto; background-color: #fff; border-radius: 10px; overflow: hidden;'>
                            <div style='background-color: #70c778; color: #333; text-align: center; padding: 20px 0;'>
                                <h1 style='margin: 0; font-size: 24px;'>Babag EcoHub Waste Collection Reminder!</h1>
                            </div>
                            <div style='padding: 20px;'>
                                <p style='font-size: 16px;'>Hi, Official {firstName}! This is Babag EcoHub, an incident has been reported within your barangay.</p>
                                <p style='font-size: 16px;'>Check our website at <a href='https://localhost:7097/'>https://localhost:7097/Account/Login</a> to approve this report ({report}).</p>
                                < p style='font-size: 16px;'>Thank you for helping Brgy. Babag be safe!</p>
                            </div>
                        </div>
                    </body>
                    </html>";

            welcomeTemplate = welcomeTemplate.Replace("{firstName}", firstName).Replace("{report}", report);

            return SendEmail(recipientEmail, subject, welcomeTemplate, ref errResponse);
        }
    }
}
