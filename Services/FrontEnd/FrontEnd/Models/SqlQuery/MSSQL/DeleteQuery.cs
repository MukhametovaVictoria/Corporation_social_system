using System.Collections.Generic;

namespace SqlQuery.MSSQL
{
    public class DeleteQuery : QueryBase
    {
        private readonly string _queryType = "DELETE";
        public string QueryType { get { return _queryType; } }

        public DeleteQuery(string mainTableName)
        {
            MainTable = mainTableName;
        }

        public override string PrepareSqlString()
        {
            var newQuery = new List<string>();
            newQuery.Add(QueryType);
            newQuery.Add(From);
            newQuery.Add(MainTable);
            if (!string.IsNullOrEmpty(Filters.Trim()))
            {
                newQuery.Add(Where);
                newQuery.Add(Filters);
            }

            return string.Join(" ", newQuery);
        }
    }
}
