using System.Collections.Generic;
using static DataAccess.Common.Enums;

namespace DataAccess.Common.SqlQuery
{
    public class JoinTables
    {
        public ICollection<JoinTablesPairs> TablePairs { get; set; }
        public JoinType JoinType { get; set; }
        public LogicalOperationStrict Operation { get; set; }
        public string JoiningTableName { get; set; }
    }
}
