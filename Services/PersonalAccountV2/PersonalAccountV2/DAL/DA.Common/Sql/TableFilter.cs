using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Common.Sql
{
    public class TableFilter
    {
        public ICollection<FieldFilter> FieldsFilter { get; set; }
        public ICollection<TableFilter> InnerTableFilter { get; set; }
        public int Operation { get; set; }
    }
}
