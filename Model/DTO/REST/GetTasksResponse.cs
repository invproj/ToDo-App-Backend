using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAppBackend.Model.DTO.Common;

namespace ToDoAppBackend.Model.DTO.REST
{
    public class GetTasksResponse
    {
        public long TotalCount { get; set; }
        public List<ToDoTask> Payload { get; set; }
    }
}
