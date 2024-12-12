using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Common.Sql
{
    public class SetSqlQuery
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public object Value { get; set; }
    }
}
