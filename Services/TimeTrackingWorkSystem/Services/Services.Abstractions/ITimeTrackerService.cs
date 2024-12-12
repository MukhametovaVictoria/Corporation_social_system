using Services.Contracts.TimeTracker;

namespace Services.Abstractions
{
	public interface ITimeTrackerService
	{
		/// <summary>
		/// Получить список тайм трекеров.
		/// </summary>
		/// <returns> Список тайм трекеров. </returns>
		Task<ICollection<TimeTrackerDto>> GetAllAsync(CancellationToken cancellationToken);


		/// <summary>
		/// Получить тайм трекер.
		/// </summary>
		/// <param name="id"> Идентификатор. </param>
		/// <returns> ДТО тайм трекера. </returns>
		Task<TimeTrackerDto> GetByIdAsync(Guid id);

		/// <summary>
		/// Создать тайм трекер.
		/// </summary>
		/// <param name="creatingTimeTrackerDto"> ДТО создаваемого проекта. </param>
		Task<Guid> CreateAsync(CreatingTimeTrackerDto creatingTimeTrackerDto);

		/// <summary>
		/// Создать тайм трекер.
		/// </summary>
		/// <param name="creatingTimeTrackerDto"> ДТО создаваемого проекта. </param>
		Task<ICollection<TimeTrackerDto>> CreateRangeAsync(List<CreatingTimeTrackerDto> creatingTimeTrackerDto);

		/// <summary>
		/// Изменить тайм трекер.
		/// </summary>
		/// <param name="id"> Иентификатор. </param>
		/// <param name="updatingTimeTrackerDto"> ДТО редактируемого тайм трекера. </param>
		Task UpdateAsync(Guid id, UpdatingTimeTrackerDto updatingTimeTrackerDto);

		/// <summary>
		/// Удалить тайм трекер.
		/// </summary>
		/// <param name="id"> Идентификатор. </param>
		Task DeleteAsync(Guid id);

		Task DeleteRangeAsync(List<Guid> ids);

		/// <summary>
		/// Получить постраничный список.
		/// </summary>
		/// <param name="filterDto"> ДТО фильтра. </param>
		/// <returns> Список тайм трекеров. </returns>
		Task<ICollection<TimeTrackerDto>> GetPagedAsync(TimeTrackerFilterDto filterDto);
	}
}

