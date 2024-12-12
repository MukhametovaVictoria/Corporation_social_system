namespace DataAccess.Common.SqlQuery
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
