using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TendaGo.Web.Models
{
    public class ProveedorModel
    {
        public int Id { get; set; }

        [Display(Name = "Razon Social")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string RazonSocial { get; set; }

        [Display(Name = "Tipo Identificación")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string TipoIdentificacion { get; set; }

        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Identificacion { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public short IdPais { get; set; }

        [Display(Name = "Provincia")]
        public int IdProvincia { get; set; }

        [Display(Name = "Ciudad")]
        public int IdCiudad { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        public string Celular { get; set; }

        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Email")]
        public string Correo { get; set; }

        [Display(Name = "Estado")]
        public bool IdEstado { get; set; }
    }
}