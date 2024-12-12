using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Common.Sql
{
    public abstract class QueryBase
    {
        private readonly string _where = "WHERE";
        private readonly string _from = "FROM";
        public string From { get { return _from; } }
        public string Where { get { return _where; } }
        public string MainTable { get; set; }
        public string Filters { get; set; }
        public abstract string PrepareSqlString();
    }
}
