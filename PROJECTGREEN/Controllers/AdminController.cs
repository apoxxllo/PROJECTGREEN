using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJECTGREEN.Models;
using System.Linq;

namespace PROJECTGREEN.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
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

    }
}
