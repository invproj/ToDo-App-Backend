using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAppBackend.Model.DTO.Common;
using ToDoAppBackend.Model.DTO.REST;

namespace ToDoAppBackend.Services
{
    public interface IDeleteTaskService
    {
        public Task DeleteTask(long id);

        public Task DeleteTaskAll();
    }
}
