namespace Services.Contracts.TimeTracker
{
	public class UpdatingTimeTrackerDto
	{
		/// <summary>
		/// Описание.
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// Кол-во часов
		/// </summary>
		public int TimeAtWork { get; set; }
	}
}
