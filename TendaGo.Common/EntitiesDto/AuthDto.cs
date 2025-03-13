using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo.Common.EntitiesDto
{
    public class AuthDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Ruc { get; set; }
        public string Correo { get; set; }
        public string Alias { get; set; }
        public string Token { get; set; }
    }
}
