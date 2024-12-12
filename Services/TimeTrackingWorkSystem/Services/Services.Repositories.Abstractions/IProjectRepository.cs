using Domain.Entities;
using Services.Contracts.Project;

namespace Services.Repositories.Abstractions
{
	/// <summary>
	/// Репозиторий работы с проектами.
	/// </summary>
	/// <typeparam name="TPrimaryKey"> Тип первичного ключа. </typeparam>
	public interface IProjectRepository : IRepository<Project, Guid>
	{
		/// <summary>
		/// Получить постраничный список.
		/// </summary>
		/// <param name="filterDto"> ДТО фильтра. </param>
		/// <returns> Список проектов. </returns>
		Task<List<Project>> GetPagedAsync(ProjectFilterDto filterDto);
        bool CheckCanCreate(Project model);

    }
}
