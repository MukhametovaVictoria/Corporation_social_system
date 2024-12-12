namespace Domain.Entities
{
	/// <summary>
	/// Класс проекта
	/// </summary>
	public class Project : IEntity<Guid>
	{
		/// <summary>
		/// Уникальный идентификатор.
		/// </summary>
		public Guid Id { get; init; }

		/// <summary>
		/// Название.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Код проекта.
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Тайм трекеры.
		/// </summary>
		public virtual List<TimeTracker> TimeTrackers { get; set; }
	}
}