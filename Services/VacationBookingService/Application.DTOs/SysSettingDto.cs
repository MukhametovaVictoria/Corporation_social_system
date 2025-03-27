using System.Collections.Generic;

namespace Application.DTOs
{
    public class SysSettingDto : EntityDto
    {
        public required string Code { get; set; }
        public string? Description { get; set; }
        public ICollection<SysSettingValueDto> SysSettingValues { get; set; } = new List<SysSettingValueDto>();
    }
}