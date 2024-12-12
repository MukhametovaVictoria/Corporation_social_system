using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Common.Sql
{
    public class JoinTablesPairs
    {
        public string FirstTableName { get; set; }
        public string FirstTableColumnName { get; set; }
        public string SecondTableName { get; set; }
        public string SecondTableColumnName { get; set; }
        public int ComparisonType { get; set; }

    }
}
