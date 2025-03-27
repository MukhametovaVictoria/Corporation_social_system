namespace Domain.Entities
{
    public class SysSettingValue : Entity
    {
        public Guid SysSettingsId { get; set; }
        public SysSetting? SysSettings { get; set; }
        public string? ValueType { get; set; }
        public string? Value { get; set; }
    }
}