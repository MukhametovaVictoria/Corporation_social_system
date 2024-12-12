using System.Collections.Generic;

namespace DataAccess.Common.SqlQuery
{
    public class TableWithFieldsNames
    {
        public string TableName { get; set; }
        public ICollection<string> Fields { get; set; }
    }
}
