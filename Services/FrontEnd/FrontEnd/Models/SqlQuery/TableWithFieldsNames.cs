using System.Collections.Generic;

namespace SqlQuery
{
    public class TableWithFieldsNames
    {
        public string TableName { get; set; }
        public ICollection<string> Fields { get; set; }
    }
}
