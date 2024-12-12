using AutoMapper;
using DataAccess.Common;
using DataAccess.Common.SqlQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using BusinessLogic.Abstractions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IBaseService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IBaseService service,
            ILogger<HomeController> logger,
            IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetSomeCollectionFromMapping")]
        [HttpPost]
        public IActionResult GetSomeCollectionFromMapping(MappingQuery mapping)
        {
            try
            {
                if (mapping == null || string.IsNullOrEmpty(mapping.MainTableName))
                {
                    _logger.LogError("HomeController.GetSomeCollectionFromMapping: mapping or main table name is null.");
                    return BadRequest(GetBadRequestObject("HomeController.GetSomeCollectionFromMapping: mapping or main table name is null."));
                }

                var servicePath = "";
                Constants.TableAndRepositoryPath.TryGetValue(mapping.MainTableName, out servicePath);

                if (string.IsNullOrEmpty(servicePath))
                {
                    _logger.LogError("HomeController.GetSomeCollectionFromMapping: service path is null.");
                    return BadRequest(GetBadRequestObject("HomeController.GetSomeCollectionFromMapping: service path is null."));
                }

                return new ObjectResult(Ok(_service.GetSomeCollectionFromMapping(mapping)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"HomeController.GetSomeCollectionFromMapping: {ex}."));
            }
        }

        [Route("GetSomeCollectionByIds")]
        [HttpGet]
        public IActionResult GetSomeCollectionByIds([FromBody] List<Guid> ids, string tableName)
        {
            try
            {
                if (ids == null || ids.Count == 0 || string.IsNullOrEmpty(tableName))
                {
                    _logger.LogError("HomeController.GetSomeCollectionByIds: ids collection is null or empty or table name is empty.");
                    return BadRequest(GetBadRequestObject("HomeController.GetSomeCollectionByIds: ids collection is null or empty or table name is empty."));
                }

                var servicePath = "";
                Constants.TableAndRepositoryPath.TryGetValue(tableName, out servicePath);

                if (string.IsNullOrEmpty(servicePath))
                {
                    _logger.LogError("HomeController.GetSomeCollectionByIds: service path is null.");
                    return BadRequest(GetBadRequestObject("HomeController.GetSomeCollectionByIds: service path is null."));
                }

                return new ObjectResult(Ok(_service.GetSomeCollectionByIds(ids, tableName)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"HomeController.GetSomeCollectionByIds: {ex}."));
            }
        }

        [Route("GetEntity")]
        [HttpGet]
        public IActionResult GetEntity(Guid id, string tableName)
        {
            try
            {
                if (id == Guid.Empty || string.IsNullOrEmpty(tableName))
                {
                    _logger.LogError("HomeController.GetEntity: id or table name is null.");
                    return BadRequest(GetBadRequestObject("HomeController.GetEntity: id or table name is null."));
                }

                var servicePath = "";
                Constants.TableAndRepositoryPath.TryGetValue(tableName, out servicePath);

                if (string.IsNullOrEmpty(servicePath))
                {
                    _logger.LogError("HomeController.GetEntity: service path is null.");
                    return BadRequest(GetBadRequestObject("HomeController.GetEntity: service path is null."));
                }

                return new ObjectResult(Ok(_service.GetEntity(id, tableName)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"HomeController.GetEntity: {ex}."));
            }
        }

        [Route("GetCollectionByJsonString")]
        [HttpGet]
        public IActionResult GetCollectionByJsonString(string jsonData, string jsonObjectName, string tableName)
        {
            try
            {
                if (string.IsNullOrEmpty(jsonData) || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(jsonObjectName))
                {
                    _logger.LogError("HomeController.GetCollectionByJsonString: jsonData or table name or jsonObjectName is null.");
                    return BadRequest(GetBadRequestObject("HomeController.GetCollectionByJsonString: jsonData or table name or jsonObjectName is null."));
                }

                var servicePath = "";
                Constants.TableAndRepositoryPath.TryGetValue(tableName, out servicePath);
                if (string.IsNullOrEmpty(servicePath))
                {
                    _logger.LogError("HomeController.GetCollectionByJsonString: service path is null.");
                    return BadRequest(GetBadRequestObject("HomeController.GetCollectionByJsonString: service path is null."));
                }

                var classPath = "";
                Constants.ClassPathByName.TryGetValue(jsonObjectName, out classPath);
                if (string.IsNullOrEmpty(classPath))
                {
                    _logger.LogError("HomeController.GetCollectionByJsonString: class path is null.");
                    return BadRequest(GetBadRequestObject("HomeController.GetCollectionByJsonString: class path is null."));
                }

                return new ObjectResult(Ok(_service.GetCollectionByJsonString(jsonData, jsonObjectName, tableName)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"HomeController.GetCollectionByJsonString: {ex}."));
            }
        }

        [Route("Delete")]
        [HttpDelete]
        public IActionResult Delete([FromBody] List<FieldFilter> filters, string tableName)
        {
            try
            {
                if (filters == null || filters.Count == 0 || string.IsNullOrEmpty(tableName))
                {
                    _logger.LogError("HomeController.Delete: filters or table name is null.");
                    return BadRequest(GetBadRequestObject("HomeController.Delete: filters or table name is null."));
                }

                var servicePath = "";
                Constants.TableAndRepositoryPath.TryGetValue(tableName, out servicePath);
                if (string.IsNullOrEmpty(servicePath))
                {
                    _logger.LogError("HomeController.Delete: service path is null.");
                    return BadRequest(GetBadRequestObject("HomeController.Delete: service path is null."));
                }

                return new ObjectResult(Ok(_service.Delete(filters, tableName)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"HomeController.Delete: {ex}."));
            }
        }

        private object GetBadRequestObject(string error)
        {
            return new { Status = "400", Error = error };
        }
    }
}
