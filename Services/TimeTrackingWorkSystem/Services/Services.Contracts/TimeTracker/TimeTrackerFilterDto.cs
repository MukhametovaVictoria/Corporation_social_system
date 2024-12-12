namespace Services.Contracts.TimeTracker
{
	public class TimeTrackerFilterDto
	{
		/// <summary>
		/// Проект.
		/// </summary>
		public required Guid ProjectId { get; init; }

		/// <summary>
		/// Сотрудник.
		/// </summary>
		public required Guid EmployeeId { get; init; }

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
