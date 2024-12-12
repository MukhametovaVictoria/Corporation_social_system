using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Picture : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public Employee Author { get; set; }
        public byte[] Data { get; set; }
        public string ByteAsString { get; set; }
        public Guid NewsId { get; set; }
        public News News { get; set; }
        public long Size { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
