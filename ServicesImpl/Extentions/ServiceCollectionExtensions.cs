using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAppBackend.Services;
using ToDoAppBackend.ServicesImpl;

namespace Microsoft.Extensions.DependencyInjection
{
    //todo cikkectuinSSSSSSSSSSSSSSSSSSSS
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisGetTaskService(this IServiceCollection servicesCollection)
        {
            return servicesCollection.AddSingleton<IGetTaskService, RedisGetTasksService>();
        }

        public static IServiceCollection AddRedisPostTaskService(this IServiceCollection servicesCollection)
        {
            return servicesCollection.AddSingleton<IPostTaskService, RedisPostTasksService>();
        }

        public static IServiceCollection AddRedisDeleteTaskService(this IServiceCollection servicesCollection)
        {
            return servicesCollection.AddSingleton<IDeleteTaskService, RedisDeleteTasksService>();
        }
    }
}