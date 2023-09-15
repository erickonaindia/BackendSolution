using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Application.Common
{
    public enum ResponseStatus
    {
        Error,
        Success,
        Warning,
        Info
    }

    public class BaseResponse
    {
        public bool Success { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public dynamic Data { get; set; }
    }
}
