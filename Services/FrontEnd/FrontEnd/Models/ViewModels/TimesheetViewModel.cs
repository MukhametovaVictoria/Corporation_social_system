using FrontEnd.Models.Timesheet;

namespace FrontEnd.Models
{
    public class TimesheetViewModel
    {
        public List<List<TimeTrackerModel>> TimesheetData {  get; set; }
        public List<ProjectModel> Projects { get; set; }
        public List<ProjectModel> AllProjects { get; set; }
        public List<string> WeekDays { get; set; }
        public DateOnly CurrentStartDate { get; set; }
        public DateOnly CurrentTillDate { get; set; }
        public Dictionary<int, string> Months { get; set; }
        public DateOnly FirstStart { get; set; }
        public DateOnly End {  get; set; }
        public DateOnly MaxStart { get; set; }
        public DateOnly MaxEnd { get; set; }
        public List<List<TimeTrackerModel>> CurrentTimesheetData { get; set; }
    }
}
