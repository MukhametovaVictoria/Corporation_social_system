namespace DataAccess.Common
{
    public class Enums
    {
        public enum OrderBy
        {
            ASC,
            DESC
        }

        public enum LogicalOperationStrict
        {
            AND = 0,
            OR = 1
        }

        public enum JoinType
        {
            INNER,
            LEFT,
            RIGHT,
            OUTER
        }

        public enum FilterComparisonType
        {
            Between = 0,
            IsNull = 1,
            IsNotNull = 2,
            Equal = 3,
            NotEqual = 4,
            Less = 5,
            LessOrEqual = 6,
            Greater = 7,
            GreaterOrEqual = 8,
            StartWith = 9,
            NotStartWith = 10,
            Contain = 11,
            NotContain = 12,
            EndWith = 13,
            NotEndWith = 14,
            Exists = 15,
            NotExists = 16
        }

    }
}
