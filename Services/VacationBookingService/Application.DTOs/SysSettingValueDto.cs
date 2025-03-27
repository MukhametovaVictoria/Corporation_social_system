namespace Application.DTOs
{
    public class SysSettingValueDto : EntityDto
    {
        public Guid SysSettingsId { get; set; }
        public SysSettingDto? SysSettings { get; set; }
        public string? ValueType { get; set; }
        public string? Value { get; set; }
    }
}