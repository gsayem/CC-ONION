using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCentreWeb.Models
{
    public class ResponseMessage
    {
        public ResponseMessage()
        {

        }
        public ResponseMessage(string message)
        {
            Message = message;
        }
        public ResponseMessage(string code, string message)
        {
            Message = message;
            Code = code;
        }
        public ResponseMessage(string code, string message, object data)
        {
            Message = message;
            Code = code;
            Data = data;
        }
        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public enum Result
    {
        Success = 1,
        Failure = 2,
        Warning = 3
    }
}
