using System;
using System.ComponentModel.DataAnnotations;

namespace CAAS
{
    public class ErrorResponse
    {

        public ErrorResponse(Exception ex)
        {
            ExceptionType = ex.GetType().FullName;
            ErrorMessage = ex.Message;
            StackTrace = ex.StackTrace;
        }

        [Required]
        public string ExceptionType { set; get; }
        [Required]
        public string ErrorMessage { set; get; }
        [Required]
        public string StackTrace { set; get; }
    }
}
