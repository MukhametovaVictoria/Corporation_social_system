using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using static DataAccess.Common.Enums;

namespace DataAccess.Common.SqlQuery.MSSQL
{
    public static class SqlScriptPreparerHelper
    {
        /// <summary>
        /// Получение скрипта удаления
        /// </summary>
        /// <param name="mapping">Маппинг удаления</param>
        /// <returns></returns>
        public static string GetDeleteQuery(MappingQuery mapping)
        {
            if (string.IsNullOrEmpty(mapping.MainTableName))
                return null;

            var deleteQuery = new DeleteQuery(mapping.MainTableName);
            deleteQuery.Filters = mapping.TableFilter == null ? "" : GetFilters(mapping.TableFilter);

            return deleteQuery.PrepareSqlString();
        }

        /// <summary>
        /// Получение скрипта обновления
        /// </summary>
        /// <param name="mapping">Маппинг обновления</param>
        /// <returns></returns>
        public static string GetUpdateQuery(MappingQuery mapping)
        {
            if (mapping == null || string.IsNullOrEmpty(mapping.MainTableName) || mapping.Sets == null || mapping.Sets.Count == 0)
                return null;

            var updateQuery = new UpdateQuery(mapping.MainTableName);
            updateQuery.ColumnsWithValues = GetSets(mapping.Sets);
            updateQuery.Filters = mapping.TableFilter == null ? "" : GetFilters(mapping.TableFilter);

            return updateQuery.PrepareSqlString();
        }

        /// <summary>
        /// Получение скрипта запроса данных
        /// </summary>
        /// <param name="mapping">Маппинг запроса</param>
        /// <returns></returns>
        public static string GetSelectQuery(MappingQuery mapping)
        {
            if (mapping == null || string.IsNullOrEmpty(mapping.MainTableName))
                return null;

            var selectQuery = new SelectQuery(mapping.MainTableName);
            selectQuery.Columns = mapping.Tables == null || mapping.Tables.Count == 0 ? "*" : GetColumns(mapping.Tables);
            selectQuery.Joins = mapping.Joins == null || mapping.Joins.Count == 0 ? "" : GetJoins(mapping.Joins);
            selectQuery.Filters = mapping.TableFilter == null ? "" : GetFilters(mapping.TableFilter);
            selectQuery.OrdersBy = mapping.OrderBy == null || mapping.OrderBy.Count == 0 ? "" : GetOrderBy(mapping.OrderBy);
            selectQuery.Offset = mapping.Offset;
            selectQuery.Fetch = mapping.Fetch;

            return selectQuery.PrepareSqlString();
        }

        /// <summary>
        /// Получить строку установок новых значений
        /// </summary>
        /// <param name="sets">Маппинг устновок значений колонок</param>
        /// <returns></returns>
        private static string GetSets(ICollection<SetSqlQuery> sets)
        {
            var setsString = new List<string>();
            foreach (var set in sets)
            {
                if (!string.IsNullOrEmpty(set.ColumnName))
                {
                    setsString.Add(set.ColumnName + " = \'" + set.Value.ToString() + "\'");
                }
            }

            return string.Join(", ", setsString);
        }

        /// <summary>
        /// Получить строку сортировки
        /// </summary>
        /// <param name="orderBySqls">Маппинг сортировок по колонкам</param>
        /// <returns></returns>
        private static string GetOrderBy(ICollection<OrderBySqlQuery> orderBySqls)
        {
            var columns = new List<string>();
            foreach (var orderBy in orderBySqls)
            {
                columns.Add(orderBy.TableName + '.' + orderBy.ColumnName + " " + orderBy.OrderBy.ToString());
            }

            return string.Join(", ", columns);
        }

        /// <summary>
        /// Получение колонок для sql запроса
        /// </summary>
        /// <param name="tables">Таблицы</param>
        /// <returns>Строка sql</returns>
        private static string GetColumns(ICollection<TableWithFieldsNames> tables)
        {
            var columns = new List<string>();
            foreach (var table in tables)
            {
                if (table.Fields == null)
                {
                    columns.Add("*");
                }
                else
                {
                    foreach (var column in table.Fields)
                    {
                        columns.Add(table.TableName + '.' + column);
                    }
                }
            }

            return string.Join(", ", columns);
        }

        /// <summary>
        /// Создание джойнов sql
        /// </summary>
        /// <param name="joinTables">Маппинг джойнов</param>
        /// <returns>Строка sql</returns>
        private static string GetJoins(ICollection<JoinTables> joinTables)
        {
            var joins = new List<string>();
            foreach (var join in joinTables)
            {
                var joinValues = new List<string>();
                foreach (var pair in join.TablePairs)
                {
                    var values = new List<string>();
                    values.Add(pair.FirstTableName + "." + pair.FirstTableColumnName);
                    values.Add(GetOperation(pair.ComparisonType));
                    values.Add(pair.SecondTableName + "." + pair.SecondTableColumnName);

                    joinValues.Add(string.Join(" ", values));
                }

                var joinStr = new List<string>();
                joinStr.Add(join.JoinType.ToString());
                joinStr.Add("JOIN");
                joinStr.Add(join.JoiningTableName);
                joinStr.Add("ON");
                joinStr.Add(string.Join(" " + join.Operation.ToString() + " ", joinValues));

                joins.Add(string.Join(" ", joinStr));
            }

            return string.Join("\n ", joins);
        }

        //Надо допилить оставшиеся операции
        /// <summary>
        /// Создание строки фильтров по маппингу
        /// </summary>
        /// <param name="map">Маппинг</param>
        /// <returns>Строка sql</returns>
        private static string GetFilters(TableFilter map)
        {
            if (map == null)
                return "";

            var newGroup = new List<string>();
            var operation = ' ' + ((LogicalOperationStrict)map.Operation).ToString() + ' ';

            if (map.InnerTableFilter != null)
            {
                foreach (var mapping in map.InnerTableFilter)
                {
                    newGroup.Add(GetFilters(mapping));
                }
            }

            if (map.FieldsFilter != null)
            {
                foreach (var field in map.FieldsFilter)
                {
                    var tableName = field.TableName;
                    if (field.Data != null && field.Data.Count > 0)
                    {
                        if (field.ComparisonType == (int)FilterComparisonType.Equal)
                        {
                            if (field.Data.Count == 1)
                            {
                                newGroup.Add(tableName + '.' + field.Name + GetOperation(field.ComparisonType) + "\'" + field.Data.First().ToString() + "\'");
                            }
                            else
                            {
                                var values = string.Join(", ", field.Data.Select(x => "\'" + x.ToString() + "\'").ToArray());
                                newGroup.Add(tableName + '.' + field.Name + " in (" + values + ")");
                            }
                        }
                        else if (field.ComparisonType == (int)FilterComparisonType.Contain)
                        {
                            newGroup.Add(tableName + '.' + field.Name + " like \'%" + field.Data.First().ToString() + "%\'");
                        }
                        else if (field.ComparisonType == (int)FilterComparisonType.Between)
                        {
                            if (field.Data.Count == 2)
                            {
                                newGroup.Add(tableName + '.' + field.Name + " >= \'" + field.Data.First().ToString() + "\'");
                                newGroup.Add(tableName + '.' + field.Name + " <= \'" + field.Data.Last().ToString() + "\'");
                            }
                        }
                        else if (field.ComparisonType == (int)FilterComparisonType.Greater ||
                                field.ComparisonType == (int)FilterComparisonType.GreaterOrEqual ||
                                field.ComparisonType == (int)FilterComparisonType.Less ||
                                field.ComparisonType == (int)FilterComparisonType.LessOrEqual)
                        {
                            newGroup.Add(tableName + '.' + field.Name + GetOperation(field.ComparisonType) + "\'" + field.Data.First().ToString() + "\'");
                        }
                        else if (field.ComparisonType == (int)FilterComparisonType.NotEqual)
                        {
                            if (field.Data.Count == 1)
                            {
                                newGroup.Add(tableName + '.' + field.Name + GetOperation(field.ComparisonType) + "\'" + field.Data.First().ToString() + "\'");
                            }
                            else
                            {
                                var values = string.Join(", ", field.Data.Select(x => "\'" + x.ToString() + "\'").ToArray());
                                newGroup.Add(tableName + '.' + field.Name + " not in (" + values + ")");
                            }
                        }
                    }
                    else
                    {
                        if (field.ComparisonType == (int)FilterComparisonType.IsNull || field.ComparisonType == (int)FilterComparisonType.IsNotNull)
                        {
                            newGroup.Add(tableName + '.' + field.Name + GetOperation(field.ComparisonType));
                        }
                    }
                }
            }

            if (newGroup.Count == 0)
                return "";

            return '(' + string.Join(operation, newGroup) + ')';
        }

        /// <summary>
        /// Получить строку оператора сравнения по типу
        /// </summary>
        /// <param name="type">Тип сравнения</param>
        /// <returns></returns>
        private static string GetOperation(int type)
        {
            if (type == (int)FilterComparisonType.Equal)
            {
                return " = ";
            }
            if (type == (int)FilterComparisonType.NotEqual)
            {
                return " != ";
            }
            if (type == (int)FilterComparisonType.Less)
            {
                return " < ";
            }
            if (type == (int)FilterComparisonType.Greater)
            {
                return " > ";
            }
            if (type == (int)FilterComparisonType.LessOrEqual)
            {
                return " <= ";
            }
            if (type == (int)FilterComparisonType.GreaterOrEqual)
            {
                return " >= ";
            }
            if (type == (int)FilterComparisonType.IsNotNull)
            {
                return " IS NOT NULL ";
            }
            if (type == (int)FilterComparisonType.IsNull)
            {
                return " IS NULL ";
            }

            return null;
        }
    }
}
