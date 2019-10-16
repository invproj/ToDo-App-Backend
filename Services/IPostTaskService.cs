using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAppBackend.Model.DTO.Common;
using ToDoAppBackend.Model.DTO.REST;

namespace ToDoAppBackend.Services
{
    public interface IPostTaskService
    {
        public Task<ToDoTask> AddTaskAsync(
            long id,
            string name,
            string description);

        public Task<ToDoTask> ChangeTaskStatusAsync(long id);
    }
}
