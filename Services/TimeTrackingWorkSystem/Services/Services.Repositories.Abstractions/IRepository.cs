using Domain.Entities;

namespace Services.Repositories.Abstractions
{
	public interface IRepository<TEntity, TPrimaryKey> where TEntity : IEntity<TPrimaryKey> 
	{
		/// <summary>
		/// Запросить все сущности в базе.
		/// </summary>
		/// <param name="noTracking"> Вызвать с AsNoTracking.</param>
		/// <returns> IQueryable массив сущностей.</returns>
		IQueryable<TEntity> GetAll(bool noTracking = false);

		/// <summary>
		/// Запросить все сущности в базе.
		/// </summary>
		/// <param name="cancellationToken"> Токен отмены. </param>
		/// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
		/// <returns> Список сущностей. </returns>
		Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);

		/// <summary>
		/// Получить сущность по Id.
		/// </summary>
		/// <param name="id"> Id сущности. </param>
		/// <returns> Cущность. </returns>
		TEntity Get(TPrimaryKey id);

		/// <summary>
		/// Получить сущность по Id.
		/// </summary>
		/// <param name="id"> Id сущности. </param>
		/// <param name="cancellationToken"></param>
		/// <returns> Cущность. </returns>
		Task<TEntity> GetAsync(TPrimaryKey id, CancellationToken cancellationToken);

		/// <summary>
		/// Удалить сущность.
		/// </summary>
		/// <param name="id"> Id удалённой сущности. </param>
		/// <returns> Была ли сущность удалена. </returns>
		bool Delete(TPrimaryKey id);

		/// <summary>
		/// Удалить сущность.
		/// </summary>
		/// <param name="entity"> Cущность для удаления. </param>
		/// <returns> Была ли сущность удалена. </returns>
		bool Delete(TEntity entity);

		/// <summary>
		/// Удалить сущности.
		/// </summary>
		/// <param name="entities"> Коллекция сущностей для удаления. </param>
		/// <returns> Была ли операция удаления успешна. </returns>
		bool DeleteRange(ICollection<TEntity> entities);

		/// <summary>
		/// Для сущности проставить состояние - что она изменена.
		/// </summary>
		/// <param name="entity"> Сущность для изменения. </param>
		void Update(TEntity entity);

		/// <summary>
		/// Добавить в базу одну сущность.
		/// </summary>
		/// <param name="entity"> Сущность для добавления. </param>
		/// <returns> Добавленная сущность. </returns>
		TEntity Add(TEntity entity);

		/// <summary>
		/// Добавить в базу одну сущность.
		/// </summary>
		/// <param name="entity"> Сущность для добавления. </param>
		/// <returns> Добавленная сущность. </returns>
		Task<TEntity> AddAsync(TEntity entity);

		/// <summary>
		/// Добавить в базу массив сущностей.
		/// </summary>
		/// <param name="entities"> Массив сущностей. </param>
		void AddRange(List<TEntity> entities);

		/// <summary>
		/// Добавить в базу массив сущностей.
		/// </summary>
		/// <param name="entities"> Массив сущностей. </param>
		Task AddRangeAsync(ICollection<TEntity> entities);

		/// <summary>
		/// Сохранить изменения.
		/// </summary>
		void SaveChanges();

		/// <summary>
		/// Сохранить изменения.
		/// </summary>
		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
