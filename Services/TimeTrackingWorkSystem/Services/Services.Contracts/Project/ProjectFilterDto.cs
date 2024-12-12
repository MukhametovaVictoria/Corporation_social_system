namespace Services.Contracts.Project
{
	public class ProjectFilterDto
	{
		/// <summary>
		/// Название проекта.
		/// </summary>
		public string Name { get; init; }
		
		/// <summary>
		/// Код проекта.
		/// </summary>
		public string Code { get; init; }

		public int ItemsPerPage { get; set; }

		public int Page { get; set; }
	}
}
