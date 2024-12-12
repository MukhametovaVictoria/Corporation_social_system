namespace FrontEnd.Models.Timesheet
{
	public class ProjectModel
	{

		/// <summary>
		/// Id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Название.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Код.
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Тайм трекеры.
		/// </summary>
		public List<TimeTrackerModel> TimeTrackers { get; set; }
	}
}