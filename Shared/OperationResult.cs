using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachines.Shared
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public OperationResult() { }

        public OperationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static OperationResult Success() => new OperationResult(true, "Успешно");
        public static OperationResult Failure(string error) => new OperationResult(false, error);
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }

        public OperationResult() : base() { }

        public OperationResult(bool isSuccess, string errorMessage, T data)
        : base(isSuccess, errorMessage)
        {
            Data = data;
        }

        public static OperationResult<T> Success(T data)
            => new OperationResult<T>(true, "Успешно", data);

        // Здесь используем new, чтобы скрыть статический метод родителя
        public static new OperationResult<T> Failure(string error)
            => new OperationResult<T>(false, error, default);
    }
}