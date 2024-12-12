using System.Collections.Generic;

namespace DataAccess.Common.SqlQuery
{
    public class FieldFilter
    {
        public string Name { get; set; }
        public ICollection<object> Data { get; set; }
        public string DataType { get; set; }
        public int ComparisonType { get; set; }
        public string TableName { get; set; }
    }
}
