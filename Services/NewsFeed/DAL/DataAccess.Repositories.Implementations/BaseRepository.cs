using DataAccess.Common;
using DataAccess.Common.SqlQuery;
using DataAccess.Common.SqlQuery.MSSQL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json;
using DataAccess.Context;

namespace DataAccess.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly DataContext _dataContext;
        public BaseRepository(DataContext context)
        {
            _dataContext = context;
        }

        /// <summary>
        /// Получение коллекции сущностей по маппингу с расширенными фильтрами
        /// </summary>
        /// <param name="mapping">Маппинг</param>
        /// <returns>Коллекция сущностей</returns>
        public object GetSomeCollectionFromMapping(MappingQuery mapping)
        {
            if (mapping == null || string.IsNullOrEmpty(mapping.MainTableName))
                return null;

            var repositoryPath = Constants.TableAndRepositoryPath[mapping.MainTableName];

            if (repositoryPath == null)
                return null;

            var invoker = new Invoker(repositoryPath, "GetCollection", false,
                new[] { new Tuple<Type, object>(_dataContext.GetType(), _dataContext) },
                new[] { new Tuple<Type, object>(mapping.GetType(), mapping) });
            
            return invoker.InvokeMethod();
        }

        /// <summary>
        /// Получение коллекции сущностей по списку Id с указанием таблицы поиска
        /// </summary>
        /// <param name="ids">Список id</param>
        /// <param name="tableName">Название таблицы</param>
        /// <returns>Коллекция сущностей</returns>
        public object GetSomeCollectionByIds(ICollection<Guid> ids, string tableName)
        {
            if (ids == null || ids.Count == 0 || string.IsNullOrEmpty(tableName))
                return null;

            var repositoryPath = Constants.TableAndRepositoryPath[tableName];
            if (string.IsNullOrEmpty(repositoryPath))
                return null;

            var invoker = new Invoker(repositoryPath, "GetCollection", false,
                new[] { new Tuple<Type, object>(_dataContext.GetType(), _dataContext) },
                new[] { new Tuple<Type, object>(ids.GetType(), ids) });

            return invoker.InvokeMethod();
        }

        /// <summary>
        /// Получение сущности по id с указанием таблицы поиска
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="tableName">Таблица</param>
        /// <returns>Сущность</returns>
        public object GetEntity(Guid id, string tableName)
        {
            if (id == Guid.Empty || string.IsNullOrEmpty(tableName))
                return null;

            var servicePath = Constants.TableAndRepositoryPath[tableName];
            if (string.IsNullOrEmpty(servicePath))
                return null;

            var invoker = new Invoker(servicePath, "Get", false,
                new[] { new Tuple<Type, object>(_dataContext.GetType(), _dataContext) },
                new[] { new Tuple<Type, object>(id.GetType(), id) });

            return invoker.InvokeMethod();
        }

        /// <summary>
        /// Получение коллекции по json запросу
        /// </summary>
        /// <param name="jsonData">Запрос</param>
        /// <param name="jsonObjectName">Объект, в который десериализуется запрос</param>
        /// <param name="tableName">Таблица поиска</param>
        /// <returns>Коллекция сущностей</returns>
        public object GetCollectionByJsonString(string jsonData, string jsonObjectName, string tableName)
        {
            if (string.IsNullOrEmpty(jsonData) || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(jsonObjectName))
                return null;

            if(!Constants.TableAndRepositoryPath.ContainsKey(tableName))
                return null;
            var repoPath = Constants.TableAndRepositoryPath[tableName];

            if (!Constants.ClassPathByName.ContainsKey(jsonObjectName))
                return null;
            var classPath = Constants.ClassPathByName[jsonObjectName];

            var type = Type.GetType(classPath);

            if (type == null)
                return null;

            var obj = JsonSerializer.Deserialize(jsonData, type);
            if (obj != null)
            {
                var repoType = Type.GetType(repoPath);
                var methods = repoType.GetMethods();
                var invoker = new Invoker(repoType.FullName.ToString(), "GetCollection", false,
                new[] { new Tuple<Type, object>(_dataContext.GetType(), _dataContext) },
                new[] { new Tuple<Type, object>(type, obj) });
                return invoker.InvokeMethod();
            }

            return null;
        }

        /// <summary>
        /// Удаление данных по фильтрам с указанием таблицы
        /// </summary>
        /// <param name="filters">Фильтры</param>
        /// <param name="tableName">Таблица</param>
        /// <returns>Количество удаленных записей</returns>
        public object Delete(List<FieldFilter> filters, string tableName)
        {
            if (filters == null || filters.Count == 0 || string.IsNullOrEmpty(tableName))
                return null;

            if (!Constants.TableAndRepositoryPath.ContainsKey(tableName))
                return null;
            var repoPath = Constants.TableAndRepositoryPath[tableName];

            var mapping = new MappingQuery()
            {
                MainTableName = tableName,
                TableFilter = new TableFilter()
                {
                    FieldsFilter = filters
                }
            };

            var deleteQuery = SqlScriptPreparerHelper.GetDeleteQuery(mapping);
            return ((DbContext)_dataContext).Database.ExecuteSqlRaw(deleteQuery);
        }
    }
}
