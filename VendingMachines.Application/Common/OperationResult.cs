using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachines.Application.Common
{
    public class OperationResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }

        protected OperationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static OperationResult Success() => new OperationResult(true, "Успешно");
        public static OperationResult Failure(string error) => new OperationResult(false, error);
    }
}
