using DA.Common.Sql;

namespace DA.Repositories.Abstractions
{
    public interface IBaseRepository
    {

        object GetSomeCollectionFromMapping(MappingQuery mapping);


        object GetSomeCollectionByIds(ICollection<Guid> ids, string tableName);


        object GetEntity(Guid id, string tableName);


        object GetCollectionByJsonString(string jsonData, string jsonObjectName, string tableName);

        object Delete(List<FieldFilter> filters, string tableName);
    }
}
