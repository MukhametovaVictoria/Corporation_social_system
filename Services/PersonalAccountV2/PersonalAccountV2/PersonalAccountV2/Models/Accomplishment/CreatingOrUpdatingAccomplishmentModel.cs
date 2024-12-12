using BS.Contracts.Base;

namespace PersonalAccountV2.Models.Accomplishment
{
    public class CreatingOrUpdatingAccomplishmentModel
    {
        public Guid Id { get; set; }
        public TypeAccomplishment Type { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public Guid EmployeeId { get; set; }
    }

}
