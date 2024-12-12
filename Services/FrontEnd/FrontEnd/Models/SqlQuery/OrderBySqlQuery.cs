using static SqlQuery.Enums;

namespace SqlQuery
{
    public class OrderBySqlQuery
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public OrderBy OrderBy { get; set; }
    }
}
