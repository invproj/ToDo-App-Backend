using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAppBackend.Model.DTO.Common;

namespace ToDoAppBackend.Model.DTO.REST
{
    public class GetTaskResponse
    {
        public ToDoTask Payload { get; set; }
    }
}
