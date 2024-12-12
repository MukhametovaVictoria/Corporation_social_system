using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Common.Sql
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
