using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Common.Sql
{
    public class MappingQuery
    {
        public string MainTableName { get; set; }
        public TableFilter TableFilter { get; set; }
        public ICollection<TableWithFieldsNames> Tables { get; set; }
        public ICollection<JoinTables> Joins { get; set; }
        public ICollection<OrderBySqlQuery> OrderBy { get; set; }
        public ICollection<SetSqlQuery> Sets { get; set; }
        public int Offset { get; set; }
        public int Fetch { get; set; }

    }
}
