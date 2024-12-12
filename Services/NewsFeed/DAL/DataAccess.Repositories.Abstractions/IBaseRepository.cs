using System;
using System.Collections.Generic;
using DataAccess.Common.SqlQuery;

namespace DataAccess.Repositories
{
    public interface IBaseRepository
    {
        /// <summary>
        /// Получение коллекции сущностей по маппингу с расширенными фильтрами
        /// </summary>
        /// <param name="mapping">Маппинг</param>
        /// <returns>Коллекция сущностей</returns>
        object GetSomeCollectionFromMapping(MappingQuery mapping);

        /// <summary>
        /// Получение коллекции сущностей по списку Id с указанием таблицы поиска
        /// </summary>
        /// <param name="ids">Список id</param>
        /// <param name="tableName">Название таблицы</param>
        /// <returns>Коллекция сущностей</returns>
        object GetSomeCollectionByIds(ICollection<Guid> ids, string tableName);

        /// <summary>
        /// Получение сущности по id с указанием таблицы поиска
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="tableName">Таблица</param>
        /// <returns>Сущность</returns>
        object GetEntity(Guid id, string tableName);

        /// <summary>
        /// Получение коллекции по json запросу
        /// </summary>
        /// <param name="jsonData">Запрос</param>
        /// <param name="jsonObjectName">Объект, в который десериализуется запрос</param>
        /// <param name="tableName">Таблица поиска</param>
        /// <returns>Коллекция сущностей</returns>
        object GetCollectionByJsonString(string jsonData, string jsonObjectName, string tableName);

        /// <summary>
        /// Удаление данных по фильтрам с указанием таблицы
        /// </summary>
        /// <param name="filters">Фильтры</param>
        /// <param name="tableName">Таблица</param>
        /// <returns>Количество удаленных записей</returns>
        object Delete(List<FieldFilter> filters, string tableName);
    }
}
