namespace Domain.Entities
{
    public class SysSetting : Entity
    {
        public required string Code { get; set; }
        public string? Description { get; set; }
        public ICollection<SysSettingValue> SysSettingValues { get; set; } = new List<SysSettingValue>();
    }
}