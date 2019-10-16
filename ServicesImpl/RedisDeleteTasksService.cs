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
    public class RedisDeleteTasksService : IDeleteTaskService
    {
        private readonly ILogger _logger;
        
        public async Task DeleteTask(long id) 
        {
            RedisProvider _redis = new RedisProvider();
            var _key = $"{Constants.TASK_PREFIX}{Convert.ToString(id)}";
            _redis.Delete(_key);
        }

        public async Task DeleteTaskAll()
        {
            RedisProvider _redis = new RedisProvider();
            _redis.DeleteAll();
        }

    }
}