using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo.Common.EntitiesDto
{
    public class ResultList<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Length { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public bool Success { get; set; }
    }
}
