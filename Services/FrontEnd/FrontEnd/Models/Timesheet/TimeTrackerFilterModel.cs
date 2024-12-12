namespace FrontEnd.Models.Timesheet
{
	public class TimeTrackerFilterModel
	{
		/// <summary>
		/// Проект.
		/// </summary>
		public required Guid ProjectId { get; set; }

		/// <summary>
		/// Сотрудник.
		/// </summary>
		public required Guid EmployeeId { get; set; }

		/// <summary>
		/// Описание.
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// Дата.
		/// </summary>
		public DateOnly Date { get; set; }

		/// <summary>
		/// Дата с которой искать.
		/// </summary>
		public DateOnly StartDate { get; set; }

		/// <summary>
		/// Дата до которой искать.
		/// </summary>
		public DateOnly TillDate { get; set; }

		/// <summary>
		/// Кол-во часов
		/// </summary>
		public int TimeAtWork { get; set; }

		public int ItemsPerPage { get; set; }

		public int Page { get; set; }
	}
}
