namespace Services.Contracts.Project
{
	/// <summary>
	/// ДТО проекта.
	/// </summary>
	public class UpdatingProjectDto
	{
		/// <summary>
		/// Название.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Код.
		/// </summary>
		public string Code { get; set; }
	}
}
