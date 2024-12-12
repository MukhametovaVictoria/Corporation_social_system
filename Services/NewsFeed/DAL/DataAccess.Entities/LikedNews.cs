using System;

namespace DataAccess.Entities
{
    public class LikedNews : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid NewsId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
