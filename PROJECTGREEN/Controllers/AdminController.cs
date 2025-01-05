using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROJECTGREEN.Manager;
using PROJECTGREEN.Models;
using System.Linq;

namespace PROJECTGREEN.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        public readonly MailManager _mailManager;
        public AdminController(MailManager mailManager)
        {
            _mailManager = mailManager;
        }
        [Authorize]
        public IActionResult Dashboard()
        {
            var wasteVolumeData = _wasteVolumeDatumRepo.Table.FirstOrDefault();

            // Get the incidents from the repository
            var purokNames = new[] {
                "Purok Super Sunlight", "Purok Beauty in the Sky", "Purok Rambo", "Purok Butterfly 1",
                "Purok Butterfly 2", "Purok Shooting Star", "Purok Judas Belt", "Purok Thunder",
                "Purok Five-Star", "Purok Bombshell", "Skina Trigon", "Skina Radar", "Skina Naga"
            };

            var incidents = _incidentRepo.Table
                .Where(i => purokNames.Any(p => i.Location.Contains(p))) // Filter only relevant locations
                .Select(i => new
                {
                    MatchedPurok = purokNames.FirstOrDefault(p => i.Location.Contains(p)), // Extract the purok name
                    i.IncidentType
                })
                .GroupBy(i => new { i.MatchedPurok, i.IncidentType }) // Group by matched purok and type of incident
                .Select(group => new IncidentFrequency
                {
                    Location = group.Key.MatchedPurok, // Use only the matched purok name
                    TypeOfIncident = group.Key.IncidentType,
                    Frequency = $"{group.Count()} incidents" // Format frequency
                })
                .OrderBy(i => i.Location) // Optional: order by location
                .ThenBy(i => i.TypeOfIncident) // Optional: order by type of incident
                .ToList();

            var topEvents = _programFeedbackRepo.Table
                .GroupBy(e => e.EventId)
                .Select(group => new TopEvent
                {
                    NameOfEvent = group.FirstOrDefault().Event.EventName,
                    AverageStar = group.Average(e => e.Stars),
                    Link = group.FirstOrDefault().Event.DocuLink
                })
                .OrderByDescending(eventGroup => eventGroup.AverageStar)
                .Take(3)
                .ToList();

            var modelView = new AdminViewModel()
            {
                Volume = wasteVolumeData,
                TopEvents = topEvents,
                IncidentFrequency = incidents  // Add the list of incident frequencies
            };

            return View(modelView);
        }

        [Authorize]
        public IActionResult WasteMetrics()
        {
            var wasteVolumeData = _wasteVolumeDatumRepo.Table.FirstOrDefault();
            var modelView = new AdminViewModel()
            {
                Volume = wasteVolumeData
            };
            return View(modelView);
        }

        [HttpPost]
        public IActionResult UpdateBiodegradableWaste(int BiodegradableWaste)
        {
            var wasteVolumeData = _wasteVolumeDatumRepo.Table.FirstOrDefault();

            wasteVolumeData.BiodegradableCollected = BiodegradableWaste;

            _wasteVolumeDatumRepo.Update(wasteVolumeData.Id, wasteVolumeData);

            TempData["message"] = "Successfully updated number of Biodegradable waste";

            return RedirectToAction("WasteMetrics");
        }
        [HttpPost]
        public IActionResult UpdateNonBiodegradableWaste(int NonBiodegradableWaste)
        {
            var wasteVolumeData = _wasteVolumeDatumRepo.Table.FirstOrDefault();

            wasteVolumeData.NonbioCollected = NonBiodegradableWaste;

            _wasteVolumeDatumRepo.Update(wasteVolumeData.Id, wasteVolumeData);

            TempData["message"] = "Successfully updated number of Non-Biodegradable waste";

            return RedirectToAction("WasteMetrics");
        }

        [HttpPost]
        public IActionResult UpdateRecyclablesCollected(int RecyclablesCollected)
        {
            var wasteVolumeData = _wasteVolumeDatumRepo.Table.FirstOrDefault();

            wasteVolumeData.RecyclablesCollected = RecyclablesCollected;

            _wasteVolumeDatumRepo.Update(wasteVolumeData.Id, wasteVolumeData);

            TempData["message"] = "Successfully updated number of Recyclables collected";

            return RedirectToAction("WasteMetrics");
        }

        [HttpPost]
        public IActionResult UpdateMonthlyRecycled(int MonthlyRecycled)
        {
            var wasteVolumeData = _wasteVolumeDatumRepo.Table.FirstOrDefault();

            wasteVolumeData.MonthlyRecycled = MonthlyRecycled;

            _wasteVolumeDatumRepo.Update(wasteVolumeData.Id, wasteVolumeData);

            TempData["message"] = "Successfully updated Monthly Recycled Volume";

            return RedirectToAction("WasteMetrics");
        }

        [HttpPost]
        public IActionResult UpdateUncollectedWaste(int UncollectedWaste)
        {
            var wasteVolumeData = _wasteVolumeDatumRepo.Table.FirstOrDefault();

            wasteVolumeData.UncollectedWaste = UncollectedWaste;

            _wasteVolumeDatumRepo.Update(wasteVolumeData.Id, wasteVolumeData);

            TempData["message"] = "Successfully updated Uncollected Waste";

            return RedirectToAction("WasteMetrics");
        }

        [HttpPost]
        public IActionResult UpdateEwasteRecycled(int EwasteRecycled)
        {
            var wasteVolumeData = _wasteVolumeDatumRepo.Table.FirstOrDefault();

            wasteVolumeData.EwasteRecycled = EwasteRecycled;

            _wasteVolumeDatumRepo.Update(wasteVolumeData.Id, wasteVolumeData);

            TempData["message"] = "Successfully updated E-Waste Recycled";

            return RedirectToAction("WasteMetrics");
        }

        [HttpPost]
        public IActionResult UpdateMrfProcessingCapacity(int MrfProcessingCapacity)
        {
            var wasteVolumeData = _wasteVolumeDatumRepo.Table.FirstOrDefault();

            wasteVolumeData.MrfProcessingCapacity = MrfProcessingCapacity;

            _wasteVolumeDatumRepo.Update(wasteVolumeData.Id, wasteVolumeData);

            TempData["message"] = "Successfully updated MRF Processing Capacity";

            return RedirectToAction("WasteMetrics");
        }


        public IActionResult MapsCollection()
        {
            var wasteVolumeData = _wasteVolumeDatumRepo.Table.FirstOrDefault();
            var modelView = new AdminViewModel()
            {
                Volume = wasteVolumeData
            };
            return View(modelView);
        }

        [HttpPost]
        public IActionResult AddAnnouncement(string subject, string note, DateTime date)
        {
            var newNote = new AdminNote()
            {
                Date = date,
                Subject = subject,
                Note = note
            };

            _adminNoteRepo.Create(newNote);

            TempData["message"] = "Successfully announced to the public";

            var emailsSubscribed = _emailsForNotificationAlertRepo.GetAll();

            foreach (var email in emailsSubscribed)
            {
                string errResponse = "";
                _mailManager.SendAnnouncementEmail(email.Email, subject, email.FirstName, note, date.ToString("MMMM dd, yyyy"), ref errResponse);
            }

            return RedirectToAction("MapsCollection");
        }

        [HttpPost]
        public IActionResult AlertDrivers()
        {
            var drivers = _positionAssignmentRepo.Table.Where(m => m.PositionId == 7 || m.PositionId == 9).ToList();

            foreach (var driver in drivers)
            {
                string errResponse = "";
                _mailManager.SendAlertDriverEmail(driver.User.Email, "Reminder for your Garbage Collection Schedule today!", driver.User.FirstName, ref errResponse);
            }

            TempData["message"] = "Drivers have been alerted successfully!";
            return RedirectToAction("MapsCollection");
        }
        public IActionResult GarbageReports()
        {
            var reports = _reportGarbageCollectionRepo.GetAll();
            ViewBag.Reports = reports;

            return View();
        }

        [HttpPost]
        public IActionResult RespondToReport(string ResponseMessage, int ReportId)
        {
            var report = _reportGarbageCollectionRepo.Get(ReportId);

            string errResponse = "";
            _mailManager.SendResponseToReportGarbageEmail(report.Email, "Garbage Collection Report Response", report.FirstName, ResponseMessage, ref errResponse);

            _reportGarbageCollectionRepo.Delete(ReportId);

            TempData["message"] = "Successfully responded to resident!";

            return RedirectToAction("GarbageReports");
        }

        public IActionResult Officials()
        {
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

        [HttpPost]
        public IActionResult ReassignCommittee(int committeeId, int newAssigneeId)
        {
            // Logic to reassign the committee (update the database, etc.)
            var committee = _committeeAssignmentRepo.Get(committeeId);
            if (committee != null)
            {
                var newAssignee = _userRepo.Get(newAssigneeId);
                if (newAssignee != null)
                {
                    committee.UserId = newAssigneeId; // Update the committee's assignee
                    _committeeAssignmentRepo.Update(committee.Id, committee);
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false, message = "Reassignment failed." });
        }

        [HttpPost]
        public IActionResult ReassignEmployee(int committeeId, int newAssigneeId)
        {
            // Logic to reassign the committee (update the database, etc.)
            var committee = _positionAssignmentRepo.Get(committeeId);
            if (committee != null)
            {
                var newAssignee = _userRepo.Get(newAssigneeId);
                if (newAssignee != null)
                {
                    committee.UserId = newAssigneeId; // Update the committee's assignee
                    _positionAssignmentRepo.Update(committee.Id, committee);
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false, message = "Reassignment failed." });
        }

        [HttpPost]
        public IActionResult EditOfficial(int Id, string firstName, string lastName, IFormFile ProfilePic)
        {
            var official = _userRepo.Get(Id);

            if (official != null)
            {
                official.FirstName = firstName;
                official.LastName = lastName;

                // Handle file upload for profile picture
                if (ProfilePic != null && ProfilePic.Length > 0)
                {
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Attachments");
                    var filePath = Path.Combine(uploads, Path.GetFileName(ProfilePic.FileName));

                    // Save file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ProfilePic.CopyTo(fileStream);
                    }

                    // Update profile picture path
                    official.ProfilePicPath = $"/Attachments/{Path.GetFileName(ProfilePic.FileName)}";
                }

                // Save changes to database
                _userRepo.Update(official.Id, official);
                TempData["message"] = "Successfully updated official details!";

                // Redirect or return success
                return RedirectToAction("Officials");
            }

            TempData["error"] = "Invalid ID!";
            return RedirectToAction("Officials");
        }


        public IActionResult GreenInitiativesRoles()
        {
            for (int i = 1; i <= 10; i++)
            {
                var committees = _committeeAssignmentRepo.Table.Where(m => m.CommitteeId == i).ToList();
                ViewData[$"Committees{i}"] = committees; // Use ViewData for dynamic keys
            }

            var users = _userRepo.GetAll();
            ViewBag.Users = users;

            return View();
        }

        public IActionResult EmployeeResponsibilities()
        {
            for (int i = 1; i <= 10; i++)
            {
                var committees = _positionAssignmentRepo.Table.Where(m => m.PositionId == i).ToList();
                ViewData[$"Position{i}"] = committees; // Use ViewData for dynamic keys
            }

            var users = _userRepo.GetAll();
            ViewBag.Users = users;

            return View();
        }

        public IActionResult GreenInitiativeSchedule()
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

        public IActionResult EmployeeTimeTable()
        {
            var employeeTimeTable = _employeeTimetableRepo.GetAll();
            ViewBag.EmployeeTimeTable = employeeTimeTable;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateEmployeeWorkSchedule(int id, string taskDescription, string schedule, string timeTable, string remarks)
        {
            try
            {
                // Find the RoleWorkSchedule entry by ID
                var workSchedule = _roleWorkScheduleRepo.Get(id);
                if (workSchedule == null)
                {
                    TempData["error"] = "Work schedule not found.";
                    return RedirectToAction("GreenInitiativeSchedule");
                }

                // Update the properties with the new data
                workSchedule.TaskDescription = taskDescription;
                workSchedule.Schedule = schedule;
                workSchedule.TimeTable = timeTable;
                workSchedule.Remarks = remarks;

                // Save the changes to the database
                _roleWorkScheduleRepo.Update(workSchedule.Id, workSchedule);

                // Return a success message (or a more detailed response)
                return Json(new { success = true, message = "Work schedule updated successfully." });
            }
            catch (Exception ex)
            {
                // Handle exceptions and errors
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public IActionResult UpdateTimetableSlot(int id, string time, string mondayTask, string tuesdayTask, string wednesdayTask, string thursdayTask, string fridayTask)
        {
            try
            {
                var timetableSlot = _employeeTimetableRepo.Get(id);
                if (timetableSlot == null)
                {
                    return Json(new { success = false, message = "Slot not found." });
                }

                timetableSlot.Time = time; // Usually not editable, but included here
                timetableSlot.MondayTask = mondayTask;
                timetableSlot.TuesTask = tuesdayTask;
                timetableSlot.WedTask = wednesdayTask;
                timetableSlot.ThuTask = thursdayTask;
                timetableSlot.FriTask = fridayTask;

                _employeeTimetableRepo.Update(id, timetableSlot);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult Programs()
        {
            var events = _eventRepo.GetAll();

            ViewBag.Events = events;
            return View();
        }

        [HttpPost]
        public IActionResult AddProgram(string eventName, string eventType, int noOfParticipants, string description, IFormFile photo, string location, string docuLink, DateTime dateTime)
        {
            DateTime utcNow = DateTime.UtcNow;

            // Define the timezone offset for UTC+08:00
            TimeSpan utcOffset = TimeSpan.FromHours(8); // UTC+08:00

            // Apply the timezone offset to get the local time in UTC+08:00
            Nullable<DateTime> PHTIME = utcNow + utcOffset;
            var newProgram = new Event()
            {
                EventName = eventName,
                Type = eventType,
                Date = PHTIME,
                Description = description,
                Location = location,
                DocuLink = docuLink,
                NoOfParticipants = noOfParticipants
            };

            if (photo != null && photo.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Attachments");
                var filePath = Path.Combine(uploads, Path.GetFileName(photo.FileName));

                // Save file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }

                // Update profile picture path
                newProgram.PhotoPath = $"/Attachments/{Path.GetFileName(photo.FileName)}";
            }


            _eventRepo.Create(newProgram);

            TempData["message"] = "Successfully added new event!";
            return RedirectToAction("Programs");
        }


        [HttpPost]
        public IActionResult DeleteEvent(int id)
        {
            var eventToDelete = _eventRepo.Get(id);

            if (eventToDelete != null)
            {
                _eventRepo.Delete(eventToDelete.Id);
                TempData["message"] = "Successfully deleted event!";
                return RedirectToAction("Programs");
            }
            TempData["error"] = "Invalid event!";
            return RedirectToAction("Programs");
        }

        public IActionResult PendingIncidents()
        {
            var incidents = _incidentRepo.Table.Where(m => m.Status.Equals("PENDING")).ToList();

            ViewBag.Incidents = incidents;
            return View();
        }

        // Approve an incident
        [HttpPost]
        public IActionResult ApproveIncident(int incidentId, string status)
        {
            var incident = _incidentRepo.Get(incidentId);
            if (incident != null)
            {
                incident.Status = status; // Update the status to APPROVED
                _incidentRepo.Update(incident.Id, incident);
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Incident not found" });
        }

        // Disapprove an incident (Delete the incident)
        [HttpPost]
        public IActionResult DisapproveIncident(int incidentId)
        {
            var incident = _incidentRepo.Get(incidentId);
            if (incident != null)
            {
                _incidentRepo.Delete(incident);
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Incident not found" });
        }

        public IActionResult AllIncidents()
        {
            var incidents = _incidentRepo.Table.Where(m => m.Status.Equals("APPROVED")).ToList();

            ViewBag.Incidents = incidents;
            return View();
        }

        [HttpPost]
        public IActionResult EditIncident(int Id, string incidentType, DateTime Date, string Location, string Impact, string Reason, string Resolution, string DocuLink)
        {
            var incident = _incidentRepo.Get(Id);

            if(incident != null)
            {
                incident.DocumentationLink = DocuLink;
                incident.Location = Location;
                incident.IncidentType = incidentType;
                incident.Resolution = Resolution;
                incident.Date = Date;
                incident.Impact = Impact;
                incident.Reason = Reason;

                _incidentRepo.Update(incident.Id, incident);

                TempData["message"] = "Successfully updated incident";
                return RedirectToAction("AllIncidents");
            }

            TempData["error"] = "Invalid incident!";
            return RedirectToAction("AllIncidents");
        }

        [HttpPost]
        public IActionResult DeleteIncident(int IncidentId)
        {
            var incident = _incidentRepo.Get(IncidentId);
            if (incident != null)
            {
                _incidentRepo.Delete(incident.Id);
                TempData["message"] = "Successfully deleted incident";
                return RedirectToAction("AllIncidents");
            }
            TempData["error"] = "Invalid incident!";
            return RedirectToAction("AllIncidents");
        }


    }



}
