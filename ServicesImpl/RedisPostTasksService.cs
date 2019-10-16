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
    public class RedisPostTasksService : IPostTaskService
    {
        private readonly ILogger _logger;
        //todo add from di
        private readonly RedisProvider _redis;

        public async Task<ToDoTask> AddTaskAsync(
            long id,
            string name,
            string description) 
        {
            RedisProvider _redis = new RedisProvider();

            var _task = new ToDoTask
            {
                Id = id,
                Name = name,
                Description = description
            };

            var key = $"{Constants.TASK_PREFIX}{ Convert.ToString(id) }";
            _redis.Set(key, _task);         
            
            return _task;
        }

        public async Task<ToDoTask> ChangeTaskStatusAsync(long id)
        {
            RedisProvider _redis = new RedisProvider();
            //todo rename with ctrl + R + R
            var _key = $"{Constants.TASK_PREFIX}{Convert.ToString(id)}";
            var _data = _redis.Get<ToDoTask>(_key);

            _data.IsDone = !(_data.IsDone);

            _redis.Set(_key, _data);

            return _data;
        }
    }
}