using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// {
    ///   "results": [
    ///     {
    ///       "id": 1,
    ///       "text": "Option 1"
    ///     },
    ///     {
    ///     "id": 2,
    ///       "text": "Option 2"
    ///     }
    ///   ],
    ///   "pagination": {
    ///     "more": true
    ///   }
    /// }

    /// </remarks>
    public class Select2ViewModel
    {
        public IEnumerable<Select2Result> results { get; set; }
        public Select2Pagination pagination { get; set; }

        public Select2ViewModel(IEnumerable<Select2Result> items = null, bool more = false)
        {
            pagination = new Select2Pagination(more);
            results = items ?? new List<Select2Result>();
        }
    }

    public class Select2Result
    {
        public string id { get; set; }
        public string text { get; set; }
        public bool selected { get; set; }
        public bool disabled { get; set; }

        public IEnumerable<Select2Result> children { get; set; }
    }

    public class Select2Pagination
    {
        public Select2Pagination(bool more)
        {
            this.more = more;
        }

        public bool more { get; set; }
    }

}
