using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAppBackend.Model.DTO.Common;
using System.ComponentModel.DataAnnotations;

namespace ToDoAppBackend.Model.DTO.REST
{
    public class AddTaskParams
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
