namespace Domain.Entities
{
	public class TimeTracker : IEntity<Guid>
	{
		/// <summary>
		/// Id.
		/// </summary>
		public Guid Id { get; init; }

		/// <summary>
		/// Проект.
		/// </summary>
		public required Guid ProjectId { get; init; }
		
		/// <summary>
		/// Проект.
		/// </summary>
		public virtual Project Project { get; set; }

		/// <summary>
		/// Сотрудник.
		/// </summary>
		public required Guid EmployeeId { get; init; }

		/// <summary>
		/// Сотрудник.
		/// </summary>
		public virtual Employee Employee { get; set; }

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
