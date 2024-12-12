using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.Project;
using Services.Repositories.Abstractions;

namespace Infrastructure.Rep.Impl
{
	/// <summary>
	/// Репозиторий работы с проектами.
	/// </summary>
	public class ProjectRepository : Repository<Project, Guid>, IProjectRepository
	{
		public ProjectRepository(DataContext context) : base(context)
		{
		}

		/// <summary>
		/// Получить сущность по Id.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns> Проект. </returns>
		public override async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
		{
			var query = Context.Set<Project>().Include(c => c.TimeTrackers).AsQueryable();
			return await query.ToListAsync(cancellationToken);
		}

		/// <summary>
		/// Получить сущность по Id.
		/// </summary>
		/// <param name="id"> Id сущности. </param>
		/// <param name="cancellationToken"></param>
		/// <returns> Проект. </returns>
		public override async Task<Project> GetAsync(Guid id, CancellationToken cancellationToken)
		{
			var query = Context.Set<Project>().Include(c => c.TimeTrackers).AsQueryable();
			return await query.SingleOrDefaultAsync(
				c => c.Id == id,
				cancellationToken);
		}

		/// <summary>
		/// Получить постраничный список.
		/// </summary>
		/// <param name="filterDto"> ДТО фильтра. </param>
		/// <returns> Список проектов. </returns>
		public async Task<List<Project>> GetPagedAsync(ProjectFilterDto filterDto)
		{
			var query = GetAll().Include(c => c.TimeTrackers).AsQueryable();
			if (!string.IsNullOrWhiteSpace(filterDto.Code))
			{
				query = query.Where(c => c.Code == filterDto.Code);
			}

			if (!string.IsNullOrWhiteSpace(filterDto.Name))
			{
				query = query.Where(c => c.Name.Contains(filterDto.Name));
			}

			if (filterDto.Page > 0)
			{
				query = query.Skip((filterDto.Page - 1) * filterDto.ItemsPerPage);
			}
			if (filterDto.ItemsPerPage > 0)
			{
				query = query.Take(filterDto.ItemsPerPage);
			}

			return await query.ToListAsync();
		}

        public bool CheckCanCreate(Project model)
        {
            var collection = GetAll()
                .Where(x => model.Code == x.Code)
                .ToList();

            return collection == null || collection.Count == 0;
        }
    }
}
