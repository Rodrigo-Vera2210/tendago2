using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Web.Models
{
    public class RespuestaViewModel
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }
        public object Data { get; set; }
    }
}