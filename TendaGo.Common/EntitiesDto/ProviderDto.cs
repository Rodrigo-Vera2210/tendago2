using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{

    public class EntityDto
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string TipoEntidad { get; set; }
        public string TipoIdentificacion { get; set; }
        [Display(Name = "Razon Social")]
        public string RazonSocial { get; set; }
        public string Identificacion { get; set; }
        public short? IdPais { get; set; }
        public string Pais { get; set; }
        public int? IdProvincia { get; set; }
        public string Provincia { get; set; }
        public int? IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public int? IdSector { get; set; }
        public string Sector { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Observaciones { get; set; }
        public string Foto { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public bool EsCliente { get; set; }
        public bool EsProveedor { get; set; }
        public string Latitud { get; set; }
        public string Longitud{ get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string EstadoCivil { get; set; }
        public string Nacionalidad { get; set; }
        public string NivelEstudio { get; set; }
        public string Profesion { get; set; }
        public bool EsConsumidorFinal { get; set; }
        public bool Full { get; set; }
    }

    public class ProviderDto : EntityDto
    {
        public string TextoBusqueda => $"{Identificacion}{RazonSocial}";
    }
    public class ClientDto : EntityDto {}
}