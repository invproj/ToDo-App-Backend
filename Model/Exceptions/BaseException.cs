using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAppBackend.Model.Exceptions
{
    public class BaseException : Exception
    {
        public string ErrorMessage { get; set; }

        public BaseException(string message)
        {
            this.ErrorMessage = message;
        }

    }
}
