using Domain.Entities;
using Services.Contracts.TimeTracker;

namespace Services.Repositories.Abstractions
{
	/// <summary>
	/// Репозиторий работы с тайм трекерами.
	/// </summary>
	public interface ITimeTrackerRepository : IRepository<TimeTracker, Guid>
	{
		/// <summary>
		/// Получить список тайм трекеров.
		/// </summary>
		/// <param name="filterDto"> ДТО фильтра. </param>
		/// <returns> Список тайм трекеров. </returns>
		Task<List<TimeTracker>> GetPagedAsync(TimeTrackerFilterDto filterDto);
		bool CheckCanCreate(TimeTracker model);

    }
}