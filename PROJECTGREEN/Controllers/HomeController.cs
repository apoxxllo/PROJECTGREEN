using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJECTGREEN.Manager;
using PROJECTGREEN.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PROJECTGREEN.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public readonly MailManager _mailManager;
        public HomeController(MailManager mailManager)
        {
            _mailManager = mailManager;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult WasteManagement()
        {
            var wasteVolumeData = _wasteVolumeDatumRepo.Table.FirstOrDefault();
            return View(wasteVolumeData);
        }
        public IActionResult EmployeeWorkSchedules()
        {
            var num1 = _roleWorkScheduleRepo.Table.Where(m => m.EmployeeRoleId == 1 || m.EmployeeRoleId == 2).ToList();
            var num2 = _roleWorkScheduleRepo.Table.Where(m => m.EmployeeRoleId == 3 || m.EmployeeRoleId == 4 || m.EmployeeRoleId == 5).ToList();
            var num3 = _roleWorkScheduleRepo.Table.Where(m => m.EmployeeRoleId == 6 || m.EmployeeRoleId == 7 || m.EmployeeRoleId == 8).ToList();
            var num4 = _roleWorkScheduleRepo.Table.Where(m => m.EmployeeRoleId == 9 || m.EmployeeRoleId == 10).ToList();
            var num5 = _roleWorkScheduleRepo.Table.Where(m => m.EmployeeRoleId == 11 || m.EmployeeRoleId == 12).ToList();

            ViewBag.Num1 = num1;
            ViewBag.Num2 = num2;
            ViewBag.Num3 = num3;
            ViewBag.Num4 = num4;
            ViewBag.Num5 = num5;

            return View();
        }
        public IActionResult EmployeeTimetable()
        {
            var employeeTimeTable = _employeeTimetableRepo.GetAll();
            ViewBag.EmployeeTimeTable = employeeTimeTable;

            return View();
        }

        [HttpPost]
        public IActionResult Subscribe(string firstName, string lastName, string email, string dayOfWeek, int zone)
        {
            var emailInSubscribers = _emailsForNotificationAlertRepo.Table.Where(m => m.Email == email).FirstOrDefault();
            if (emailInSubscribers != null)
            {
                TempData["error"] = "The email you've entered has already been subscribed";
                return RedirectToAction("WasteManagement"); // Redirect to the appropriate page
            }
            var newEmail = new EmailsForNotificationAlert()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                DayOfTheWeek = dayOfWeek,
                Zone = zone
            };

            _emailsForNotificationAlertRepo.Create(newEmail);

            TempData["message"] = "You have successfully subscribed for email alerts!";
            return RedirectToAction("WasteManagement"); // Redirect to the home page or any page
        }

        [HttpPost]
        public IActionResult Unsubscribe(string email)
        {
            // Remove the email from the subscription list
            var emailInSubscribers = _emailsForNotificationAlertRepo.Table.Where(m => m.Email == email).FirstOrDefault();
            if(emailInSubscribers == null)
            {
                TempData["error"] = "The email you've entered was not subscribed";
                return RedirectToAction("WasteManagement"); // Redirect to the appropriate page
            }
            _emailsForNotificationAlertRepo.Delete(emailInSubscribers.Id);

            TempData["message"] = "You have successfully unsubscribed from email alerts.";
            return RedirectToAction("WasteManagement"); // Redirect to the appropriate page

        }

        public IActionResult Organization()
        {
            for (int i = 1; i <= 10; i++)
            {
                var committees = _committeeAssignmentRepo.Table.Where(m => m.CommitteeId == i).ToList();
                ViewData[$"Committees{i}"] = committees; // Use ViewData for dynamic keys
            }
            for (int i = 1; i <= 10; i++)
            {
                var committees = _positionAssignmentRepo.Table.Where(m => m.PositionId == i).ToList();
                ViewData[$"Position{i}"] = committees; // Use ViewData for dynamic keys
            }

            var punongBarangay = _userRepo.Table.Where(m => m.BarangayPositionId == 1).FirstOrDefault();
            ViewBag.Captain = punongBarangay;

            var councilors = _userRepo.Table.Where(m => m.BarangayPositionId == 2).ToList();
            ViewBag.Councilors = councilors;

            var secretary = _userRepo.Table.Where(m => m.BarangayPositionId == 3).FirstOrDefault();
            ViewBag.Secretary = secretary;

            var treasurer = _userRepo.Table.Where(m => m.BarangayPositionId == 4).FirstOrDefault();
            ViewBag.Treasurer = treasurer;

            var skChairman = _userRepo.Table.Where(m => m.BarangayPositionId == 5).FirstOrDefault();
            ViewBag.SkChairman = skChairman;

            var skCouncilors = _userRepo.Table.Where(m => m.BarangayPositionId == 6).ToList();
            ViewBag.SkCouncilors = skCouncilors;

            var skSecretary = _userRepo.Table.Where(m => m.BarangayPositionId == 7).FirstOrDefault();
            ViewBag.SKSecretary = skSecretary;

            var skTreasurer = _userRepo.Table.Where(m => m.BarangayPositionId == 8).FirstOrDefault();
            ViewBag.SKTreasurer = skTreasurer;


            return View();
        }




        public IActionResult CommunityInvolvementPrograms()
        {
            var events = _eventRepo.GetAll();

            ViewBag.Events = events;
            return View();
        }

        [HttpPost]
        public IActionResult AddFeedback(int id, int rating, string comment, string? firstName, string? lastName)
        {
            // Validate input
            if (rating < 1 || rating > 5 || string.IsNullOrWhiteSpace(comment))
            {
                TempData["error"] = "Invalid feedback. Please provide a rating and a comment.";
                return RedirectToAction("CommunityInvolvementPrograms"); // Redirect back to the events page
            }

            DateTime utcNow = DateTime.UtcNow;

            // Define the timezone offset for UTC+08:00
            TimeSpan utcOffset = TimeSpan.FromHours(8); // UTC+08:00

            // Apply the timezone offset to get the local time in UTC+08:00
            Nullable<DateTime> PHTIME = utcNow + utcOffset;
            // Create a feedback object or save it directly to the database
            var feedback = new ProgramFeedback
            {
                EventId = id,
                Stars = rating,
                Comment = comment,
                SubmittedAt = PHTIME,
                FirstName = firstName,
                LastName = lastName
            };

            _programFeedbackRepo.Create(feedback);

            // Add a success message
            TempData["message"] = "Thank you for your feedback!";
            return RedirectToAction("CommunityInvolvementPrograms"); // Redirect back to the events page
        }

        public IActionResult Incidents()
        {
            var incidents = _incidentRepo.Table.Where(m => m.Status.Equals("APPROVED")).ToList();

            ViewBag.Incidents = incidents;
            return View();
        }

        [HttpPost]
        public IActionResult AddComment(int incidentId, string comment)
        {
            if (string.IsNullOrEmpty(comment))
            {
                TempData["error"] = "Comment must not be empty";
                return RedirectToAction("Incidents");
            }
            DateTime utcNow = DateTime.UtcNow;

            // Define the timezone offset for UTC+08:00
            TimeSpan utcOffset = TimeSpan.FromHours(8); // UTC+08:00

            // Apply the timezone offset to get the local time in UTC+08:00
            Nullable<DateTime> PHTIME = utcNow + utcOffset;

            var incident = _incidentRepo.Get(incidentId);
            if (incident != null)
            {
                var newComment = new IncidentFeedback
                {
                    IncidentId = incidentId,
                    Comment = comment,
                    Date = PHTIME
                };

                _incidentFeedbackRepo.Create(newComment);
            }

            TempData["message"] = "Comment added successfully!";
            return RedirectToAction("Incidents");
        }


        [HttpPost]
        public ActionResult ReportIncident(string type, DateTime datetime, string location)
        {

            // Save the incident to the database
            var incident = new Incident
            {
                IncidentType = type,
                Date = datetime,
                Location = location,
                Status = "PENDING"
            };

            // Call a service to save the incident

            _incidentRepo.Create(incident);
            // Redirect to the Incidents page with a success message
            string errResponse = "";
            var captain = _userRepo.Table.Where(m => m.BarangayPositionId == 1).FirstOrDefault();
            _mailManager.SendAlertAdminEmail(captain.Email, "Incident Report", captain.FirstName, type, ref errResponse);
            TempData["message"] = "Incident reported successfully!";
            return RedirectToAction("Incidents");


        }
    }
}
