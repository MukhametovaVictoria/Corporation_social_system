using Services.Contracts.Project;
using System.Threading;

namespace Services.Abstractions
{
	/// <summary>
	/// Интерфейс сервиса работы с проектами.
	/// </summary>
	public interface IProjectService
	{
		/// <summary>
		/// Получить список проектов.
		/// </summary>
		/// <returns> Список проектов. </returns>
		Task<ICollection<ProjectDto>> GetAllAsync(CancellationToken cancellationToken);


		/// <summary>
		/// Получить проект.
		/// </summary>
		/// <param name="id"> Идентификатор. </param>
		/// <returns> ДТО проекта. </returns>
		Task<ProjectDto> GetByIdAsync(Guid id);

		/// <summary>
		/// Создать проект.
		/// </summary>
		/// <param name="creatingProjectDto"> ДТО создаваемого проекта. </param>
		Task<Guid> CreateAsync(CreatingProjectDto creatingProjectDto);

		/// <summary>
		/// Изменить проект.
		/// </summary>
		/// <param name="id"> Иентификатор. </param>
		/// <param name="updatingProjectDto"> ДТО редактируемого проекта. </param>
		Task UpdateAsync(Guid id, UpdatingProjectDto updatingProjectDto);

		/// <summary>
		/// Удалить проект.
		/// </summary>
		/// <param name="id"> Идентификатор. </param>
		Task DeleteAsync(Guid id);

		/// <summary>
		/// Получить постраничный список.
		/// </summary>
		/// <param name="filterDto"> ДТО фильтра. </param>
		/// <returns> Список проектов. </returns>
		Task<ICollection<ProjectDto>> GetPagedAsync(ProjectFilterDto filterDto);
	}
}