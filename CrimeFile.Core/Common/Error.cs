using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.Common
{
    public class Error
    {
        public string Code { get; set; }

        public string Message
        {
            get
            {
                string message = "No Code";
                if (!string.IsNullOrEmpty(Code))
                {
                    message = Code;
                }
                return message;
            }
        }

        public ErrorException Exception { get; set; }

        public Error()
        {
        }

        public Error(string code)
        {
            Code = code;
        }

        public Error(Exception ex)
        {
            //TODO: must be configurable
            Exception = new ErrorException { Message = ex.Message, StackTrace = ex.StackTrace };
        }

        public Error(string code, Exception ex)
        {
            Code = code;

            //TODO: must be configurable
            Exception = new ErrorException { Message = ex.Message, StackTrace = ex.StackTrace };
        }

        public Error(string code, Exception ex, string referenceId = "")
        {
            Code = code;

            //TODO: must be configurable
            Exception = new ErrorException { Message = ex.Message, StackTrace = ex.StackTrace, ReferenceId = referenceId };
        }
    }

    public class ErrorException
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public string ReferenceId { get; set; }
    }
}
