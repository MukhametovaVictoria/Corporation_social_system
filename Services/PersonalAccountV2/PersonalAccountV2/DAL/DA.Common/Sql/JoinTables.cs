using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DA.Common.Enums;

namespace DA.Common.Sql
{
    public class JoinTables
    {
        public ICollection<JoinTablesPairs> TablePairs { get; set; }
        public JoinType JoinType { get; set; }
        public LogicalOperationStrict Operation { get; set; }
        public string JoiningTableName { get; set; }
    }
}
