namespace Domain.Entities
{
	/// <summary>
	/// Класс сотрудник
	/// </summary>
	public class Employee : IEntity<Guid>
	{
		/// <summary>
		/// Уникальный идентификатор.
		/// </summary>
		public Guid Id { get; init; }

		/// <summary>
		/// Имя.
		/// </summary>
		public required string FirstName { get; set; }
		
		/// <summary>
		/// Фамилия.
		/// </summary>
		public string SurnName { get; set; }
		
		/// <summary>
		/// Позиция.
		/// </summary>
		public string Position { get; set; }

		/// <summary>
		/// Список таймтрекеров по проекту.
		/// </summary>
		/// <param name="project">Проект.</param>
		/// <returns>Возвращает список таймтрекеров по проекту.</returns>
		public IEnumerable<TimeTracker> GetTimeTrackingProjectList(Project project)
		{
			return Enumerable.Empty<TimeTracker>();
		}

		/// <summary>
		/// Список таймтрекеров.
		/// </summary>
		/// <returns>Возвращает список таймтрекеров.</returns>
		public IEnumerable<TimeTracker> GetTimeTrackingFilterList()
		{
			return Enumerable.Empty<TimeTracker>();
		}

		/// <summary>
		/// Обновить таймтрекер.
		/// </summary>
		/// <param name="timeTracker">Таймтрекер.</param>
		/// <param name="hours">Сколько часов.</param>
		/// <param name="date">Дата.</param>
		/// <returns>Возвращает true, если обновлено удачно, иначе false.</returns>
		public bool Update(TimeTracker timeTracker, int hours, DateTime date)
		{
			return true;
		}

		/// <summary>
		/// Удалить тайм трекер.
		/// </summary>
		/// <param name="timeTracker">Таймтрекер.</param>
		/// <returns>Возвращает true, если удалено удачно, иначе false.</returns>
		public bool Delete(TimeTracker timeTracker)
		{
			return true;
		}
	}
}