namespace WebApi.Models.TimeTracking
{
    public class CreatingTimeTrackerModel
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
		/// Кол-во часов
		/// </summary>
		public int TimeAtWork { get; set; }
	}
}
