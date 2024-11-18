﻿namespace FrontEnd.Models.Timesheet
{
    public class UpdatingTimeTrackerModel
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
