using Services.Contracts.TimeTracker;

namespace Services.Contracts.Project
{
	/// <summary>
	/// ДТО проекта.
	/// </summary>
	public class ProjectDto
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
		public List<TimeTrackerDto> TimeTrackers { get; set; }
	}
}