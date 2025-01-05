using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Mail;
using PROJECTGREEN.Manager;
using PROJECTGREEN.Models;
using PROJECTGREEN.Repositories;
using System.Net.Sockets;

public class EmailMonitoringBackgroundTask : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IServiceProvider _serviceProvider;

    public EmailMonitoringBackgroundTask(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Background email service started.");
        // Run the email checks every minute
        _timer = new Timer(PerformEmailCheck, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        return Task.CompletedTask;
    }

    private void PerformEmailCheck(object state)
    {
        DateTime utcNow = DateTime.UtcNow;

        // Define the timezone offset for UTC+08:00
        TimeSpan utcOffset = TimeSpan.FromHours(8); // UTC+08:00

        // Apply the timezone offset to get the local time in UTC+08:00
        Nullable<DateTime> currentTime = utcNow + utcOffset;// Convert to PHT (UTC + 8)
        var dayOfWeek = currentTime.Value.DayOfWeek;

        // Check if it's 9 AM or 1 PM PHT and not a holiday
        if (((currentTime.Value.Hour == 9 && currentTime.Value.Minute == 0) || (currentTime.Value.Hour == 13 && currentTime.Value.Minute == 0))
                && !IsHoliday(currentTime))
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var emailService = scope.ServiceProvider.GetRequiredService<MailManager>();
                var emailsForNotifAlertRepo = scope.ServiceProvider.GetRequiredService<BaseRepository<EmailsForNotificationAlert>>();
                var subscribers = GetSubscribersForToday(dayOfWeek, emailsForNotifAlertRepo);

                foreach (var subscriber in subscribers)
                {
                    var subject = "Waste Collection Reminder";
                    string zone = "";
                    string errResponse = "";
                    if (subscriber.Zone == 1)
                    {
                        zone = "Zone 1: Highway/Road Zone";
                    }
                    else if (subscriber.Zone == 2)
                    {
                        zone = "Zone 2: Residential Zone";
                    }
                    // Send the email to each subscriber
                    emailService.SendAlertEmail(subscriber.Email, subject, subscriber.FirstName, subscriber.DayOfTheWeek, zone, ref errResponse);
                }
            }
        }
    }

    private IEnumerable<EmailsForNotificationAlert> GetSubscribersForToday(DayOfWeek dayOfWeek, BaseRepository<EmailsForNotificationAlert> emailsForNotifAlertRepo)
    {
        // This assumes you have a Subscribers table with a 'DayOfWeek' field
        return emailsForNotifAlertRepo.Table.Where(m => m.DayOfTheWeek == dayOfWeek.ToString()).ToList();
    }

    // Static holiday list for the Philippines (Fixed Date)
    private bool IsHoliday(Nullable<DateTime> date)
    {
        // List of fixed holidays
        var holidays = new[]
        {
            new DateTime(date.Value.Year, 1, 1),  // New Year's Day
            new DateTime(date.Value.Year, 4, 9),  // Araw ng Kagitingan
            new DateTime(date.Value.Year, 5, 1),  // Labor Day
            new DateTime(date.Value.Year, 6, 12), // Independence Day
            new DateTime(date.Value.Year, 11, 30), // Bonifacio Day
            new DateTime(date.Value.Year, 12, 25), // Christmas Day
            new DateTime(date.Value.Year, 12, 30), // Rizal Day
        };

        // Check for movable holidays (e.g., National Heroes Day - last Monday of August)
        var nationalHeroesDay = GetLastMondayOfAugust(date.Value.Year);
        holidays = holidays.Concat(new[] { nationalHeroesDay }).ToArray();

        // Add special non-working days like Christmas Eve, New Year's Eve, etc.
        holidays = holidays.Concat(new[]
        {
            new DateTime(date.Value.Year, 12, 24), // Christmas Eve
            new DateTime(date.Value.Year, 12, 31), // New Year's Eve
        }).ToArray();

        return holidays.Contains(date.Value.Date);
    }

    private DateTime GetLastMondayOfAugust(int year)
    {
        var lastDayOfAugust = new DateTime(year, 8, 31);
        var daysToSubtract = (lastDayOfAugust.DayOfWeek == DayOfWeek.Monday) ? 0 : (int)lastDayOfAugust.DayOfWeek + 1;
        return lastDayOfAugust.AddDays(-daysToSubtract);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
