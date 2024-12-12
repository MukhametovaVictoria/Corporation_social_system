namespace FrontEnd.Models.PersonalAccountModels
{
    public class AccomplishmentModel
    {
        public Guid Id { get; set; }
        public TypeAccomplishment Type { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public EmployeeModelFromPA? Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }

}
