using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Web.Models
{
    public class ApiErrorResponse
    {
        public string TechnicalMessage { get; set; }
        public string UserMessage { get; set; }
        public string ErrorCode { get; set; }
    }

    public class GeneriApiErrorResponse
    {
        public string Message { get; set; }
    }

}