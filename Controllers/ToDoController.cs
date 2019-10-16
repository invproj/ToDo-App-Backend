using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ToDoAppBackend.Model.DTO.Common;
using ToDoAppBackend.Model.DTO.REST;
using ToDoAppBackend.Model.Exceptions;
using ToDoAppBackend.Providers;
using ToDoAppBackend.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoAppBackend.Controllers
{
    [Route("api/v1/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IGetTaskService _getTaskService;
        private readonly IPostTaskService _postTaskService;
        private readonly IDeleteTaskService _deleteTaskService;

        public ToDoController(
            ILogger<ToDoController> logger,
            IGetTaskService getTaskService,
            IPostTaskService postTaskService,
            IDeleteTaskService deleteTaskService)
        {
            _logger = logger;
            _getTaskService = getTaskService;
            _postTaskService = postTaskService;
            _deleteTaskService = deleteTaskService;
        }

        [HttpGet("hello-world")]
        public IEnumerable<ToDoTask> HelloWorld()
        {
            var rng = new Random();
            return Enumerable.Range(3, 5).Select(index => new ToDoTask
            {
                //Id = index,
                Name = "hello",
                Description = "world",
                IsDone = false,
            }).ToArray();
        }

        [HttpGet("get-tasks")]
        public async Task<GetTasksResponse> GetTasks()
        {
            try
            {
                return await _getTaskService.GetTasksAsync();
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
        }

        [HttpGet("get-task")]
        public async Task<GetTaskResponse> GetTaskDetails([FromQuery] long id)
        {
            try
            {
                #region validation
                if (id == 0)
                {
                    throw ToDoExceptions.CreateExceptionRequiredFieldIsMissing(nameof(id));
                }
                #endregion

                return await _getTaskService.GetTaskAsync(id);
            }
            catch (BaseException ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                // debug
                RedisProvider _redis = new RedisProvider();
                _redis.Set<BaseException>("last-exception", ex);
                ///

                return null;
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
        }

        [HttpPost("add-task")]
        public async Task<ToDoTask> AddTask([FromBody] AddTaskParams parameters)
        {
            try
            {
                #region validation

                if (parameters == null)
                {
                    throw ToDoExceptions.CreateExceptionBadParam(nameof(parameters), "null", "Parameters is null");
                }

                if (parameters.Id == 0)
                {
                    throw ToDoExceptions.CreateExceptionRequiredFieldIsMissing(nameof(parameters.Id));
                }

                if (parameters.Name == null)
                {
                    throw ToDoExceptions.CreateExceptionRequiredFieldIsMissing(nameof(parameters.Name));
                }

                #endregion

                return await _postTaskService.AddTaskAsync(
                    parameters.Id,
                    parameters.Name,
                    parameters.Description);
            }
            catch (BaseException ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                // debug
                RedisProvider _redis = new RedisProvider();
                _redis.Set<BaseException>("last-exception", ex);
                ///

                return null;
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
        }

        [HttpPost("change-task-status")]
        public async Task<ToDoTask> ChangeTaskStatus(long id)
        {
            try
            {
                #region validation
                if (id == 0)
                {
                    throw ToDoExceptions.CreateExceptionRequiredFieldIsMissing(nameof(id));
                }
                #endregion

                return await _postTaskService.ChangeTaskStatusAsync(id);
            }
            catch (BaseException ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                // debug
                RedisProvider _redis = new RedisProvider();
                _redis.Set<BaseException>("last-exception", ex);
                ///

                return null;
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
        }

        [HttpDelete("delete-task")]
        public async Task DeleteTask(long id)
        {
            // Todo: fix api. case: if there is nothing to delete
            try
            {
                if (id == 0)
                {
                    throw ToDoExceptions.CreateExceptionBadParam(nameof(id), id.ToString());
                }
                await _deleteTaskService.DeleteTask(id);
            }
            catch (BaseException ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                // debug
                RedisProvider _redis = new RedisProvider();
                _redis.Set<BaseException>("last-exception", ex);
                ///

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        [HttpDelete("delete-tasks")]
        public async Task DeleteAllTasks()
        {
            try
            {
                await _deleteTaskService.DeleteTaskAll();
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
