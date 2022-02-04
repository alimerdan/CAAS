using System;

namespace CAAS.ApiProvider
{
    public class ErrorResponse
    {

        public ErrorResponse(Exception ex)
        {
            ExceptionType = ex.GetType().FullName;
            ErrorMessage = ex.Message;
            StackTrace = ex.StackTrace;
        }

        public string ExceptionType { set; get; }

        public string ErrorMessage { set; get; }

        public string StackTrace { set; get; }
    }
}
