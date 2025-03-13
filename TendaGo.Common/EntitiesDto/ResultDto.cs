using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo.Common.EntitiesDto
{
    public class ResultDto<T>
    {
        public T Result { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public bool Success { get; set; }
    }


}
