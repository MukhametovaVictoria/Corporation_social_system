using static DataAccess.Common.Enums;

namespace DataAccess.Common.SqlQuery
{
    public class OrderBySqlQuery
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public OrderBy OrderBy { get; set; }
    }
}
