using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DA.Common.Enums;

namespace DA.Common.Sql
{
    public class OrderBySqlQuery
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public OrderBy OrderBy { get; set; }
    }
}
