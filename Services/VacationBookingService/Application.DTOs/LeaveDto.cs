namespace Application.DTOs
{
    public class LeaveDto : EntityDto
    {
        public Guid EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Count => (EndDate - StartDate).Days + 1;
        public bool IsPaid { get; set; }
    }
}
