using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PROJECTGREEN.Models;
using PROJECTGREEN.Repositories;

namespace PROJECTGREEN.Controllers
{
    public class BaseController : Controller
    {
        public DbAb152cProjectgreendbContext _db;
        public BaseRepository<User> _userRepo;
        public BaseRepository<ProgramFeedback> _programFeedbackRepo;
        public BaseRepository<Event> _eventRepo;
        public BaseRepository<Incident> _incidentRepo;
        public BaseRepository<EmailsForNotificationAlert> _emailsForNotificationAlertRepo;
        public BaseRepository<CommitteeAssignment> _committeeAssignmentRepo;
        public BaseRepository<PositionAssignment> _positionAssignmentRepo;
        public BaseRepository<WasteVolumeDatum> _wasteVolumeDatumRepo;
        public BaseRepository<AdminNote> _adminNoteRepo; 
        public BaseRepository<BarangayPosition> _barangayPositionRepo;
        public BaseRepository<RoleWorkSchedule> _roleWorkScheduleRepo;
        public BaseRepository<EmployeeTimetable> _employeeTimetableRepo;
        public BaseRepository<IncidentFeedback> _incidentFeedbackRepo;
        public BaseRepository<ReportGarbageCollection> _reportGarbageCollectionRepo;
        public BaseController()
        {
            _db = new DbAb152cProjectgreendbContext();
            _userRepo = new BaseRepository<User>();
            _eventRepo = new BaseRepository<Event>();
            _programFeedbackRepo = new BaseRepository<ProgramFeedback>();
            _incidentRepo = new BaseRepository<Incident>();
            _emailsForNotificationAlertRepo = new BaseRepository<EmailsForNotificationAlert>();
            _committeeAssignmentRepo = new BaseRepository<CommitteeAssignment>();
            _positionAssignmentRepo = new BaseRepository<PositionAssignment>();
            _wasteVolumeDatumRepo = new BaseRepository<WasteVolumeDatum>();
            _adminNoteRepo = new BaseRepository<AdminNote>();
            _barangayPositionRepo = new BaseRepository<BarangayPosition>();
            _roleWorkScheduleRepo = new BaseRepository<RoleWorkSchedule>();
            _employeeTimetableRepo = new BaseRepository<EmployeeTimetable>();
            _incidentFeedbackRepo = new BaseRepository<IncidentFeedback>();
            _reportGarbageCollectionRepo = new BaseRepository<ReportGarbageCollection>();
        }

    }
}
