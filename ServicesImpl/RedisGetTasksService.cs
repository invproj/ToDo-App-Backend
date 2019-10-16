using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAppBackend.Services;
using ToDoAppBackend.Model;
using ToDoAppBackend.Model.DTO.Common;
using ToDoAppBackend.Model.DTO.REST;
using Microsoft.Extensions.Logging;
using ToDoAppBackend.Providers;

namespace ToDoAppBackend.ServicesImpl
{
    //todo group into one service
    public class RedisGetTasksService : IGetTaskService
    {
        private readonly ILogger _logger;

        public async Task<GetTasksResponse> GetTasksAsync()
        {
            RedisProvider _redis = new RedisProvider();
            //todo add async
            var _data = _redis.ScanAndGet<ToDoTask>($"{Constants.TASK_PREFIX}*");
            return new GetTasksResponse
            {
                Payload = _data,
                TotalCount = _data.Count
            };
        }

        public async Task<GetTaskResponse> GetTaskAsync(long id)
        {
            RedisProvider _redis = new RedisProvider();

            //todo convert to string to tostring
            var _key = $"{Constants.TASK_PREFIX}{Convert.ToString(id)}";                       
            var _data = _redis.Get<ToDoTask>(_key);
            return new GetTaskResponse
            {
                Payload = _data,
            };

        }
    }
}