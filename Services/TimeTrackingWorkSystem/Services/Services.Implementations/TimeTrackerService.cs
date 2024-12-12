using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
using Services.Contracts.Project;
using Services.Contracts.TimeTracker;
using Services.Repositories.Abstractions;
using System.Threading;

namespace Services.Implementations
{
	public class TimeTrackerService : ITimeTrackerService
	{
		private readonly IMapper _mapper;
		private readonly ITimeTrackerRepository _timeTrackerRepository;

		public TimeTrackerService(
		   IMapper mapper,
		   ITimeTrackerRepository timeTrackerRepository)
		{
			_mapper = mapper;
			_timeTrackerRepository = timeTrackerRepository;
		}

		/// <summary>
		/// Получить тайм трекер.
		/// </summary>
		/// <returns> ДТО тайм трекера. </returns>
		public async Task<ICollection<TimeTrackerDto>> GetAllAsync(CancellationToken cancellationToken)
		{
			var timeTrackers = await _timeTrackerRepository.GetAllAsync(cancellationToken);
			return _mapper.Map<ICollection<TimeTracker>, ICollection<TimeTrackerDto>>(timeTrackers);
		}

		/// <summary>
		/// Получить тайм трекер.
		/// </summary>
		/// <param name="id"> Идентификатор. </param>
		/// <returns> ДТО тайм трекера. </returns>
		public async Task<TimeTrackerDto> GetByIdAsync(Guid id)
		{
			var timeTracker = await _timeTrackerRepository.GetAsync(id, CancellationToken.None);
			return _mapper.Map<TimeTracker, TimeTrackerDto>(timeTracker);
		}

		/// <summary>
		/// Создать тайм трекер.
		/// </summary>
		/// <param name="creatingProjectDto"> ДТО создаваемого тайм трекера. </param>
		/// <returns> Идентификатор. </returns>
		public async Task<Guid> CreateAsync(CreatingTimeTrackerDto creatingProjectDto)
        {
			var timeTracker = _mapper.Map<CreatingTimeTrackerDto, TimeTracker>(creatingProjectDto);
			var createdProject = await _timeTrackerRepository.AddAsync(timeTracker);
			await _timeTrackerRepository.SaveChangesAsync();

			return createdProject.Id;
		}

		public async Task<ICollection<TimeTrackerDto>> CreateRangeAsync(List<CreatingTimeTrackerDto> creatingTimeTrackerDto)
		{
			var timeTracker = _mapper.Map<List<CreatingTimeTrackerDto>, List<TimeTracker>>(creatingTimeTrackerDto);
			var result = new List<TimeTracker>();
			foreach(var item in timeTracker)
			{
				if (_timeTrackerRepository.CheckCanCreate(item))
				{
                    result.Add(_timeTrackerRepository.Add(item));
                }
            }
			
			await _timeTrackerRepository.SaveChangesAsync();

			return _mapper.Map<ICollection<TimeTracker>, ICollection<TimeTrackerDto>>(result);

        }

		/// <summary>
		/// Изменить тайм трекер.
		/// </summary>
		/// <param name="id"> Идентификатор. </param>
		/// <param name="updatingTimeTracker"> ДТО редактируемого тайм трекера. </param>
		public async Task UpdateAsync(Guid id, UpdatingTimeTrackerDto updatingTimeTracker)
		{
			var timeTracker = await _timeTrackerRepository.GetAsync(id, CancellationToken.None);
			if (timeTracker == null)
			{
				throw new Exception($"Тайм трекер с идентфикатором {id} не найден");
			}

			timeTracker.TimeAtWork = updatingTimeTracker.TimeAtWork;
			timeTracker.Description = updatingTimeTracker.Description;
			_timeTrackerRepository.Update(timeTracker);
			await _timeTrackerRepository.SaveChangesAsync();
		}

		/// <summary>
		/// Удалить тайм трекер.
		/// </summary>
		/// <param name="id"> Идентификатор. </param>
		public async Task DeleteAsync(Guid id)
		{
			_timeTrackerRepository.Delete(id);
            await _timeTrackerRepository.SaveChangesAsync();
        }

		public async Task DeleteRangeAsync(List<Guid> ids)
		{
			var range = (await _timeTrackerRepository.GetAllAsync(CancellationToken.None)).Where(x => ids.Contains(x.Id)).ToList();
			if(range != null && range.Count > 0)
			{
				_timeTrackerRepository.DeleteRange(range);
                await _timeTrackerRepository.SaveChangesAsync();
            }
		}

		/// <summary>
		/// Получить постраничный список.
		/// </summary>
		/// <param name="filterDto"> ДТО фильтра. </param>
		/// <returns> Список тайм трекеров. </returns>
		public async Task<ICollection<TimeTrackerDto>> GetPagedAsync(TimeTrackerFilterDto filterDto)
		{
			ICollection<TimeTracker> entities = await _timeTrackerRepository.GetPagedAsync(filterDto);
			return _mapper.Map<ICollection<TimeTracker>, ICollection<TimeTrackerDto>>(entities);
		}
	}
}
