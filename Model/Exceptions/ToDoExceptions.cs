using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAppBackend.Model.Exceptions
{
    public class ToDoExceptions
    {
        public static BaseException Create(string errorMessage = null) => new BaseException(errorMessage);

        public static BaseException CreateExceptionRequiredFieldIsMissing(string fieldName) => Create( $"Field is missing: {fieldName}");
        public static BaseException CreateExceptionBadParam(string parameter, string value, string message = "Value is wrong") => Create($"Bad parameter: {parameter}, Value: {value}, Message: {message}");
        public static BaseException CreateExceptionUnknownError() => Create("Unknown Error");

    }
}
