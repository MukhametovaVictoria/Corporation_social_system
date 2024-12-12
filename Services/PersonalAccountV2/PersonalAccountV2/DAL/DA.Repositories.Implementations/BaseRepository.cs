using DA.Common;
using DA.Common.Sql;
using DA.Common.Sql.MSsql;
using DA.Context;
using DA.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DA.Repositories.Implementations
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly DataContext _dataContext;
        public BaseRepository(DataContext context)
        {
            _dataContext = context;
        }

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


        public object GetCollectionByJsonString(string jsonData, string jsonObjectName, string tableName)
        {
            if (string.IsNullOrEmpty(jsonData) || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(jsonObjectName))
                return null;

            if (!Constants.TableAndRepositoryPath.ContainsKey(tableName))
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
