using DA.Common.Sql;

namespace BS.Services.Abstractions
{
    public interface IBaseService
    {
        object GetSomeCollectionFromMapping(MappingQuery mapping);
        object GetSomeCollectionByIds(List<Guid> ids, string tableName);
        object GetEntity(Guid id, string tableName);
        object GetCollectionByJsonString(string jsonData, string jsonObjectName, string tableName);
        object Delete(List<FieldFilter> filters, string tableName);
    }
}
