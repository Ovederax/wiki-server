using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wiki_server.dto.response
{
    public class ResponseStatus
    {
        public int Status { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public ResponseStatus(int status)
        {
            Status = status;
        }

        public ResponseStatus(int status, int errorCode, string message, Exception exception) : this(status)
        {
            ErrorCode = errorCode;
            Message = message;
            Exception = exception;
        }

        static ResponseStatus success = new ResponseStatus(0);
        public static ResponseStatus OK() 
        {
            return success;
        }
    }
}
