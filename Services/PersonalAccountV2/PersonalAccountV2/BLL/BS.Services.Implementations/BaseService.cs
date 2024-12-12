using AutoMapper;
using BS.Services.Abstractions;
using DA.Common;
using DA.Common.Sql;
using DA.Repositories.Abstractions;

namespace BS.Services.Implementations
{
    public class BaseService : IBaseService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository _baseRepository;

        public BaseService(IMapper mapper, IBaseRepository baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }
        public object GetSomeCollectionFromMapping(MappingQuery mapping)
        {
            if (mapping == null || string.IsNullOrEmpty(mapping.MainTableName))
                return null;

            var servicePath = Constants.TableAndRepositoryPath[mapping.MainTableName];

            if (servicePath == null)
                return null;

            var result = _baseRepository.GetSomeCollectionFromMapping(mapping);
            return result;
        }

        public object GetSomeCollectionByIds(List<Guid> ids, string tableName)
        {
            if (ids == null || ids.Count == 0 || string.IsNullOrEmpty(tableName))
                return null;

            var servicePath = Constants.TableAndRepositoryPath[tableName];
            if (string.IsNullOrEmpty(servicePath))
                return null;

            return _baseRepository.GetSomeCollectionByIds(ids, tableName);
        }

        public object GetEntity(Guid id, string tableName)
        {
            if (id == Guid.Empty || string.IsNullOrEmpty(tableName))
                return null;

            var servicePath = Constants.TableAndRepositoryPath[tableName];
            if (string.IsNullOrEmpty(servicePath))
                return null;

            return _baseRepository.GetEntity(id, tableName);
        }

        public object GetCollectionByJsonString(string jsonData, string jsonObjectName, string tableName)
        {
            if (string.IsNullOrEmpty(jsonData) || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(jsonObjectName))
                return null;

            var servicePath = Constants.TableAndRepositoryPath[tableName];
            if (string.IsNullOrEmpty(servicePath))
                return null;

            var classPath = Constants.ClassPathByName[jsonObjectName];
            if (string.IsNullOrEmpty(classPath))
                return null;

            return _baseRepository.GetCollectionByJsonString(jsonData, jsonObjectName, tableName);
        }

        public object Delete(List<FieldFilter> filters, string tableName)
        {
            if (filters == null || filters.Count == 0 || string.IsNullOrEmpty(tableName))
                return null;

            var servicePath = Constants.TableAndRepositoryPath[tableName];
            if (string.IsNullOrEmpty(servicePath))
                return null;

            return _baseRepository.Delete(filters, tableName);
        }
    }
}
