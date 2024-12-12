using System.Collections.Generic;
using static SqlQuery.Enums;

namespace SqlQuery
{
    public class JoinTables
    {
        public ICollection<JoinTablesPairs> TablePairs { get; set; }
        public JoinType JoinType { get; set; }
        public LogicalOperationStrict Operation { get; set; }
        public string JoiningTableName { get; set; }
    }
}
