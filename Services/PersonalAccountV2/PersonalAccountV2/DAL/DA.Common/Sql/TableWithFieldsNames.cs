using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Common.Sql
{
    public class TableWithFieldsNames
    {
        public string TableName { get; set; }
        public ICollection<string> Fields { get; set; }
    }
}
